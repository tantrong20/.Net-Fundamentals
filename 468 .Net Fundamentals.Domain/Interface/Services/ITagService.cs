using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ITagService
    {
        public Task Create(TagCreateVM request);
        public Task<IList<TagVM>> GetAll(int projectId);
        public Task Update(int id, string name);
        public Task Delete(int id);
    }
}
