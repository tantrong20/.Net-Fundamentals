using _468_.Net_Fundamentals.Domain;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class CardService : RepositoryBase<Card>, ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CardService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCard(Card card)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                card.Project = await _unitOfWork.Repository<Project>().FindAsync(1);
                card.Status = (int)EnumType.Status.Backlog;
                await _unitOfWork.Repository<Card>().InsertAsync(card);
                await _unitOfWork.CommitTransaction();
            }catch(Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task AddTodo(int Id, Todo todo)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                todo.CardId = Id;
                todo.IsCompleted = false;

                await _unitOfWork.Repository<Todo>().InsertAsync(todo);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task UpdateCardPriority(int Id, int priotity)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var oldCard = await _unitOfWork.Repository<Card>().FindAsync(Id);

                if (oldCard == null) throw new KeyNotFoundException();

                oldCard.Priority = priotity;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }


        public async Task AssignUser(int Id, int userId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var card = await _unitOfWork.Repository<Card>().FindAsync(Id);
                var user = await _unitOfWork.Repository<User>().FindAsync(userId);

                var cardAssign = new CardAssign
                {
                    Card = card,
                    User = user
                };

                await _unitOfWork.Repository<CardAssign>().InsertAsync(cardAssign);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<Card>> GetAllCard()
        {
            return await _unitOfWork.Repository<Card>().GetAllAsync();          
        }

        public async Task<Card> GetCard(int Id)
        {
            return await _unitOfWork.Repository<Card>().FindAsync(Id);
        }
    }
}
