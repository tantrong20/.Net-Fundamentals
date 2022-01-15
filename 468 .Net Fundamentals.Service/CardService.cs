using _468_.Net_Fundamentals.Domain;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _468_.Net_Fundamentals.Domain.EnumType;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Newtonsoft.Json;

namespace _468_.Net_Fundamentals.Service
{
    public class CardService : RepositoryBase<Card>, ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CardService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task Create(int busId, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Hard code for user
                var user = await _unitOfWork.Repository<User>().FindAsync(1);

                var card = await _unitOfWork.Repository<Business>()
                    .Query()
                    .Where(_ => _.Id == busId)
                    .Select(bus => new Card
                    {
                        Name = name,
                        BusinessId = bus.Id,
                        Index = bus.Cards.Count != 0 ? bus.Cards.Max(c => c.Index) + 1 : 1,
                        Priority = TaskPriority.Normal,
                        CreatedOn = DateTime.Now
                    })
                    .FirstOrDefaultAsync();

                await _unitOfWork.Repository<Card>().InsertAsync(card);
                await _unitOfWork.SaveChangesAsync();

                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    Action = AcctionEnumType.Create,
                    OnDate = DateTime.Now
                };
                await _unitOfWork.Repository<Activity>().InsertAsync(activity);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<IList<CardVM>> GetAllByBusiness(int busId)
        {
            try
            {
                var cardVMs = await _unitOfWork.Repository<Card>()
                    .Query()
                    .Where(_ => _.BusinessId == busId)
                    .OrderBy(_ => _.Index)
                    .Select(card => new CardVM
                    {
                        Id = card.Id,
                        Name = card.Name,
                        Description = card.Description,
                        Duedate = card.Duedate,
                        Priority = card.Priority,
                        BusinessId = card.BusinessId,
                        Index = card.Index
                    })
                    .ToListAsync();

                return cardVMs;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<CardVM> GetDetail(int id)
        {
            try
            {
                var cardVM = await _unitOfWork.Repository<Card>()
                    .Query()
                    .Where(_ => _.Id == id)
                    .Select(card => new CardVM
                    {
                        Id = card.Id,
                        Name = card.Name,
                        Description = card.Description,
                        Duedate = card.Duedate,
                        Priority = card.Priority,
                        BusinessId = card.BusinessId,
                        Index = card.Index

                    }).FirstOrDefaultAsync();

                return cardVM;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Hard code for user
                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                if (card == null) return;

                // Save history
                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    Action = AcctionEnumType.Delete,
                    OnDate = DateTime.Now
                };
                await _unitOfWork.Repository<Activity>().InsertAsync(activity);

                // Delete card
                await _unitOfWork.Repository<Card>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        // Update API

        public async Task CardMovement(int id, [FromBody] CardMovementVM data)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Hard code for user
                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                // Save history
                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    OnDate = DateTime.Now
                };

                if (card.BusinessId == data.BusId)
                {
                    activity.Action = AcctionEnumType.ReOrder;
                }
                else
                {
                    activity.Action = AcctionEnumType.UpdateBusiness;

                    activity.PreviousValue = card.Business.Name;
                    var business = await _unitOfWork.Repository<Business>().FindAsync(data.BusId);
                    activity.CurrentValue = business.Name;
                }

                await _unitOfWork.Repository<Activity>().InsertAsync(activity);

                // Movement
                card.BusinessId = data.BusId;
                card.Index = data.Index;


                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        public async Task UpdateName(int id, [FromBody] string newName)
        {
            try
            {

                await _unitOfWork.BeginTransaction();

                // Hardcode for user
                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                if (card.Name == newName) return;

                // Save history
                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    Action = AcctionEnumType.UpdateName,
                    PreviousValue = card.Name,
                    CurrentValue = newName,
                    OnDate = DateTime.Now
                };
                await _unitOfWork.Repository<Activity>().InsertAsync(activity);

                // Update Name
                card.Name = newName;


                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        public async Task UpdatePriority(int id, [FromBody] TaskPriority newPriority)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                if (card.Priority == newPriority) return;

                // Save history
                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    Action = AcctionEnumType.UpdatePriority,
                    PreviousValue = card.Priority.ToString(),
                    CurrentValue = newPriority.ToString(),
                   /* PreviousValue = JsonConvert.SerializeObject(
                        new { priority = card.Priority }),
                    CurrentValue = JsonConvert.SerializeObject(
                        new { priority = newPriority }),*/
                    OnDate = DateTime.Now
                };

                // Update priority
                card.Priority = newPriority;

                await _unitOfWork.Repository<Activity>().InsertAsync(activity);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        public async Task UpdateDescription(int id, [FromBody] string newDescription)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                if (card.Description == newDescription) return;

                // Save history
                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    Action = AcctionEnumType.UpdateDescription,
                    PreviousValue = card.Description,
                    CurrentValue = newDescription,
                    OnDate = DateTime.Now
                };

                // Update description
                card.Description = newDescription;

                await _unitOfWork.Repository<Activity>().InsertAsync(activity);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        public async Task UpdateDuedate(int id, [FromBody] string newDuedate)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);


                // Save history
                var activity = new Activity
                {
                    CardId = card.Id,
                    UserId = user.Id,
                    Action = AcctionEnumType.UpdateDuedate,
                    PreviousValue = card.Duedate.ToString(),
                    CurrentValue = DateTime.Parse(newDuedate).ToString(),
                    OnDate = DateTime.Now
                };

                // Update duedate
                card.Duedate = DateTime.Parse(newDuedate);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }


    }
}
