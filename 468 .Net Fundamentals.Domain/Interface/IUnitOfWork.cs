using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /*int Complete();*/

        IRepository<T> Repository<T>() where T : class;

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollbackTransaction();

        Task<int> SaveChangesAsync();

    }
}
