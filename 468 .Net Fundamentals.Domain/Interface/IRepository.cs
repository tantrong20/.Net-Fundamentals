using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<IList<T>> GetAllAsync();

        T Find(params object[] keyValues);

        Task<T> FindAsync(params object[] keyValues);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task InsertAsync(T entity, bool saveChanges = true);

        Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true);

        Task DeleteAsync(int id, bool saveChanges = true);

        Task DeleteAsync(T entity, bool saveChanges = true);

        Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true);

    }
}
