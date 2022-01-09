using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IUserService
    {
        /*public Task Create();*/
        public Task<IList<UserVM>> GetAll();
        public Task<UserVM> Get(int id);

        public Task AddCardAssign(int cardId, int userId);

        public Task<IList<CardAssignVM>> GetAllCardAssign(int cardId);


        /*public Task Update(int id, string name);
        public Task Delete(int id);*/
    }
}
