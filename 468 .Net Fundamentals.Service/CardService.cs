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
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Service.LogActivity;

namespace _468_.Net_Fundamentals.Service
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrrentUser _currrentUser;
        private readonly LoggingUserActivity _loggingUserActivity;

        public CardService(IUnitOfWork unitOfWork, ICurrrentUser currrentUser, LoggingUserActivity loggingUserActivity)
        {
            _unitOfWork = unitOfWork;
            _currrentUser = currrentUser;
            _loggingUserActivity = loggingUserActivity;
        }

        public async Task Create(int busId, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var card = await _unitOfWork.Repository<Business>()
                    .Query()
                    .Where(_ => _.Id == busId)
                    .Select(bus => new Card(name, bus.Id, bus.Cards.Count != 0 ? bus.Cards.Max(c => c.Index) + 1 : 1))
                    .FirstOrDefaultAsync();

                await _unitOfWork.Repository<Card>().InsertAsync(card);
                await _unitOfWork.SaveChangesAsync();

                // User action 
                await _loggingUserActivity.Save(card.Id, AcctionEnumType.Create);

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
                return await _unitOfWork.Repository<Card>()
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
                return await _unitOfWork.Repository<Card>()
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

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);
                // Delete card
                await _unitOfWork.Repository<Card>().DeleteAsync(id);

                // Save history
                await _loggingUserActivity.Save(card.Id, AcctionEnumType.Delete);

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

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                // To get business name
                var business = await _unitOfWork.Repository<Business>().FindAsync(data.BusId);

                // Update Card Movement
                card.UpdateMovement(data.BusId, data.Index);

                // Save history
                if (card.BusinessId == data.BusId)
                {
                    var currentValue = business.Name;
                    await _loggingUserActivity.Save(card.Id, AcctionEnumType.ReOrder, currentValue);
                }
                else
                {
                    var currentValue = business.Name;
                    var previousValue = card.Business.Name;
                    await _loggingUserActivity.Save(card.Id, AcctionEnumType.UpdateBusiness, currentValue, previousValue);
                }

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

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);
                // Update Name
                card.UpdateName(newName);

                // Save history
                var previousValue = card.Name;
                var currentValue = newName;
                await _loggingUserActivity.Save(card.Id, AcctionEnumType.UpdateName, currentValue, previousValue);

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

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                if (card.Priority == newPriority) return;

                // Update priority
                card.UpdatePriority(newPriority);

                // Save history
                var previousValue = card.Priority.ToString();
                var currentValue = newPriority.ToString();
                await _loggingUserActivity.Save(card.Id, AcctionEnumType.UpdatePriority, currentValue, previousValue);

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

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);

                if (card.Description == newDescription) return;
                // Update description
                card.UpdateDescription(newDescription);

                // Save history
                var previousValue = card.Description;
                var currentValue = newDescription;
                await _loggingUserActivity.Save(card.Id, AcctionEnumType.UpdateDescription, currentValue, previousValue);

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

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);
                // Update duedate
                card.UpdateDuedate(DateTime.Parse(newDuedate));

                // Save history
                var previousValue = card.Duedate.ToString();
                var currentValue = DateTime.Parse(newDuedate).ToString();
                await _loggingUserActivity.Save(card.Id, AcctionEnumType.UpdateDuedate, currentValue, previousValue);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }


    }
}
