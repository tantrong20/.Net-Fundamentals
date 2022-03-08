using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _468_.Net_Fundamentals.Service
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(int cardId, string name)
        {
            try
            {
                var todo = new Todo(cardId, name);
      
                await _unitOfWork.Repository<Todo>().InsertAsync(todo);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<IList<TodoVM>> GetAll(int cardId)
        {

            return await _unitOfWork.Repository<Todo>()
                .Query()
                .Where(_ => _.CardId == cardId)
                .Select(todo => new TodoVM
                {
                    Id = todo.Id,
                    IsCompleted = todo.IsCompleted,
                    Name = todo.Name,
                    CardId = todo.CardId
                }).ToListAsync();
        }

        public async Task UpdateName(int id, string name)
        {
            try
            {
                var todo = await _unitOfWork.Repository<Todo>().FindAsync(id);
                todo.UpdateName(name);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task UpdateComplete(int id, bool status)
        {
            try
            {
                var todo = await _unitOfWork.Repository<Todo>().FindAsync(id);
                todo.UpdateStatus(status);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.Repository<Todo>().DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
