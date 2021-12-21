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

      
    }
}
