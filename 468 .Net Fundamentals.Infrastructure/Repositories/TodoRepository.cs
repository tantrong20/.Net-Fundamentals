using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {

    }
    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
