using _468_.Net_Fundamentals.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IProjectService
    {
        public Task Create(Project project);
        public Task<IList<Project>> GetAll();
        public Task<Project> Get(int id);
        public Task Update(int id, string name);
        public Task Delete(int id);
    }
}
