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


namespace _468_.Net_Fundamentals.Service
{
    public class CardService : RepositoryBase<Card>, ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CardService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task Create(CardCreateVM request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = await _unitOfWork.Repository<Business>().FindAsync(request.BusId);
                                
                var card = new Card
                {
                    Name = request.Name,
                    Business = business
                };
                await _unitOfWork.Repository<Card>().InsertAsync(card);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e) 
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<CardVM>> GetAll(int busId)
        {
            var cardVMs = new List<CardVM>();

            var allcard = await _unitOfWork.Repository<Card>().GetAllAsync();

            var cards = from card in allcard where card.BusinessId == busId select card;

            foreach(var card in cards)
            {
                var cardVM = new CardVM
                {
                    /*Id = card.Id,*/
                    Name = card.Name,
                    Description = card.Description,
                    Duedate = card.Duedate,
                    Priority = card.Priority,
                    BusinessId = card.BusinessId,
                };
                cardVMs.Add(cardVM);
            }

            return cardVMs;
        }

        public async Task<CardVM> Get(int id)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            var cardVM = new CardVM
            {
                Id = card.Id,
                Name = card.Name,
                Description = card.Description,
                Duedate = card.Duedate,
                Priority = card.Priority,
                BusinessId = card.BusinessId,
            };

            return cardVM;
        }

        public async Task Update(int id, CardVM cardVM)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var card = await _unitOfWork.Repository<Card>().FindAsync(id);


                card.Name = cardVM.Name;
                card.Description = cardVM.Description;
                card.Duedate = cardVM.Duedate;
                card.Priority = cardVM.Priority;
                if (cardVM.BusinessId > 0)
                {
                    card.BusinessId = cardVM.BusinessId;

                }

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<Card>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        //CardTag
        public async Task AddTag(int id, int tagId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var card =  await _unitOfWork.Repository<Card>().FindAsync(id);
                var tag = await _unitOfWork.Repository<Tag>().FindAsync(tagId);

                var cardTag = new CardTag
                {
                    Card = card,
                    Tag = tag
                };

                await _unitOfWork.Repository<CardTag>().InsertAsync(cardTag);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task DeleteTag(int id, int tagId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var card = await _unitOfWork.Repository<Card>().FindAsync(id);
                var tag = await _unitOfWork.Repository<Tag>().FindAsync(tagId);

                var cardTag = new CardTag
                {
                    Card = card,
                    Tag = tag
                };

                await _unitOfWork.Repository<CardTag>().InsertAsync(cardTag);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public Task<IList<CardTag>> GetAllTag()
        {
            throw new NotImplementedException();
        }
    }
}
