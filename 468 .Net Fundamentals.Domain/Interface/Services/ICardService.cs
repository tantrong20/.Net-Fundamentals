using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ICardService
    {
        public Task Create(CardCreateVM request);
        public Task<IList<CardVM>> GetAll(int busId);
        public Task<CardVM> Get(int id);
        public Task Update(int id, CardVM cardVM);
        public Task Delete(int id);
        public Task AddTag(int id, int tagId);
        public Task DeleteTag(int id, int tagId);
        public Task<IList<CardTag>> GetAllTag();


    }
}
