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
using Microsoft.EntityFrameworkCore;

namespace _468_.Net_Fundamentals.Service
{
    public class BusinessService : IBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(int projectId, string name)
        {
            try
            {
                var business = new Business(projectId, name);

                await _unitOfWork.Repository<Business>().InsertAsync(business);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IList<BusinessVM>> GetAllByProject(int projectId)
        {
            try
            {

                return await _unitOfWork.Repository<Business>()
                 .Query()
                 .Where(_ => _.ProjectId == projectId)
                 .Select(b => new BusinessVM
                 {
                     Id = b.Id,
                     Name = b.Name,
                     ProjectId = b.ProjectId,
                 })
                 .ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task Update(int id, string name)
        {
            try
            {
                var business = await _unitOfWork.Repository<Business>().FindAsync(id);
                business.UpdateName(name);
            }
            catch (Exception e)
            {
                throw e;
            };
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.Repository<Business>().DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            };
        }


    }
}
