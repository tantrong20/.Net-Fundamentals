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


namespace _468_.Net_Fundamentals.Service
{
    public class TodoService : RepositoryBase<Todo>, ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(TodoCreateVM request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var todo = new Todo
                {
                    Name = request.Name,
                    CardId = request.CardId,
                    IsCompleted = false
                };

                await _unitOfWork.Repository<Todo>().InsertAsync(todo);
                
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }


        public async Task<IList<TodoVM>> GetAll(int cardId)
        {

            var alltodo = await _unitOfWork.Repository<Todo>().GetAllAsync();

            var todos = from todo in alltodo where todo.CardId == cardId select todo;

            var todoVMs = new List<TodoVM>();

            foreach (var todo in todos)
            {
                todoVMs.Add(new TodoVM { Name = todo.Name, CardId = todo.CardId });
            }

            return todoVMs;
        }

        public async Task Update(int id, TodoVM todoVM)
        {

            try
            {
                await _unitOfWork.BeginTransaction();

                var todo =  await _unitOfWork.Repository<Todo>().FindAsync(id);

                todo.Name = todoVM.Name;
                todo.IsCompleted = todoVM.IsCompleted;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<Todo>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
    }
}
