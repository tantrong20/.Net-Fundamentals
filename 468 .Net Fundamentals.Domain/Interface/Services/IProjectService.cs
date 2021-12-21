using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IProjectService
    {
        public Task Create(ProjectCreateRequest request);
        public Task<IList<Project>> GetAll();
        public Task<ProjectDetailsVM> Get(int id);
        public Task Update(int id, string name);
        public Task Delete(int id);

        // Business
        public Task CreateBusiness(int id, BusinessCreateRequest request);
        public Task UpdateBusiness(int busId ,string name);
        public Task DeleteBusiness(int busId);


    }
}


