using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class BusinessService : RepositoryBase<Business>, IBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        // Business

        public async Task CreateBusiness(BusinessVM request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = new Business
                {
                    Name = request.Name,
                    ProjectId = request.ProjectId
                };

                await _unitOfWork.Repository<Business>().InsertAsync(business);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<BusinessVM>> GetAllBusiness(int projectId)
        {
            var allBusiness = await _unitOfWork.Repository<Business>().GetAllAsync();

            var businesses = from business in allBusiness where business.ProjectId == projectId select business;

            var businessesVM = new List<BusinessVM>();

            foreach(var b in businesses)
            {
                businessesVM.Add( new BusinessVM { Name = b.Name, ProjectId = b.ProjectId });
            }

            return businessesVM;
        }

        public async Task UpdateBusiness(int id, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = await _unitOfWork.Repository<Business>().FindAsync(id);
                business.Name = name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            };
        }

        public async Task DeleteBusiness(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<Business>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            };
        }

    
    }
}
