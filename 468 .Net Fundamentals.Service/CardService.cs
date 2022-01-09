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

                var card = await _unitOfWork.Repository<Business>()
                    .Query()
                    .Where(_ => _.Id == busId)
                    .Select(bus => new Card
                    {
                        Name = name,
                        BusinessId = bus.Id,
                        Index = bus.Cards.Count > 0 ? bus.Cards[bus.Cards.Count - 1].Index + 1 : 1,
                        Priority = TaskPriority.Normal,
                        CreatedOn = DateTime.Now
                    })
                    .FirstOrDefaultAsync();
        
          
                await _unitOfWork.Repository<Card>().InsertAsync(card);
                                     
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e) 
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<CardVM>> GetAllByBusiness(int busId)
        {
            var cardVMs = await _unitOfWork.Repository<Card>()
                .Query()
                .Where(_ => _.BusinessId == busId)
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

        public async Task<CardVM> GetDetail(int id)
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

         public async Task Delete(int id)
        {   
            await _unitOfWork.Repository<Card>().DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();    
        }

        // Update API

        public async Task CardMovement(int id, [FromBody] CardMovementVM data)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.BusinessId = data.BusId;
            card.Index = data.Index;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateName(int id, [FromBody] string newName)
        {
            var card =  await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Name = newName;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePriority(int id, [FromBody] TaskPriority newPriority)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Priority = newPriority;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBusiness(int id, [FromBody] int newBusinessId)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.BusinessId = newBusinessId;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDescription(int id, [FromBody] string newDescription)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Description = newDescription;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDuedate(int id, [FromBody] string newDuedate)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Duedate = DateTime.Parse(newDuedate);

            await _unitOfWork.SaveChangesAsync();
        }

  

      
    }
}
