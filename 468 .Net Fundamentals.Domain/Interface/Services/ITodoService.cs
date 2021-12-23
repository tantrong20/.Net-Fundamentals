using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ITodoService
    {

        public Task Create(TodoCreateVM request);
        public Task<IList<TodoVM>> GetAll(int cardId);

        public Task Update(int id, TodoVM todoVM);
        public Task Delete(int id);


    }
}
