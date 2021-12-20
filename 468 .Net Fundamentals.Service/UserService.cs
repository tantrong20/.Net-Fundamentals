using _468_.Net_Fundamentals.Domain;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class UserService : RepositoryBase<User> ,IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                user.Role = (int)EnumType.Role.Employee;
                await _unitOfWork.Repository<User>().InsertAsync(user);

                await _unitOfWork.CommitTransaction();
            }catch(Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }

        }

        public async Task<IList<User>> GetAll()
        {
            return await _unitOfWork.Repository<User>().GetAllAsync();
        }

        public async Task<User> GetOne(int userId)
        {
            return await _unitOfWork.Repository<User>().FindAsync(userId);
        }

 
        public async Task<bool> IsExistAsync(int id)
        {
            var cardRepo = _unitOfWork.Repository<Card>();
            return await cardRepo.AnyAsync(_ => _.Id == id);
        }

    }
}
