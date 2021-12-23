using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IBusinessService
    {
        // Business
        public Task CreateBusiness(BusinessVM request);
        public Task<IList<BusinessVM>> GetAllBusiness(int projectId);
        public Task UpdateBusiness(int id, string name);
        public Task DeleteBusiness(int id);
    }
}
