using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ITodoService
    {

        public Task Create(int cardId, string name);
        public Task<IList<TodoVM>> GetAll(int cardId);

        public Task UpdateName(int id, string name);
        public Task UpdateComplete(int id, Boolean status);

        public Task Delete(int id);


    }
}
