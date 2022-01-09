using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class UserService : RepositoryBase<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCardAssign(int cardId, int userId)
        {
            try
            {
                var cardAssign = new CardAssign
                {
                    CardId = cardId,
                    AssignTo = userId
                };

                await _unitOfWork.Repository<CardAssign>().InsertAsync(cardAssign);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<CardAssignVM>> GetAllCardAssign(int cardId)
        {
            var cardAssignVM = await _unitOfWork.Repository<CardAssign>()
             .Query()
             .Where(_ => _.CardId == cardId)
             .Select(c => new CardAssignVM
             {
                 CardId = c.CardId,
                 AssignTo = c.AssignTo,
                 UserName = c.User.Name,
                 UserEmail = c.User.Email,
                 ImagePath = c.User.ImagePath
             }).ToListAsync();

            return cardAssignVM;
        }

        public async Task<UserVM> Get(int Id)
        {
            var userVM = await _unitOfWork.Repository<User>()
              .Query()
              .Where(_ => _.Id == Id)
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  Name = u.Name,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).FirstOrDefaultAsync();

            return userVM;
        }

        public async Task<IList<UserVM>> GetAll()
        {
            var userVMs = await _unitOfWork.Repository<User>()
              .Query()
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  Name = u.Name,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).ToListAsync();

            return userVMs;
        }

     
    }
}
