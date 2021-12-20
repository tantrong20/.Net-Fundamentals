using _468_.Net_Fundamentals.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Infrastructure
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        public DbSet<T> Entities => Context.Set<T>();

        public readonly ApplicationDbContext Context;
        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public T Find(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await Entities.FindAsync(keyValues);
        }

        public async Task InsertAsync(T entity, bool saveChanges = true)
        {
            await Entities.AddAsync(entity);

            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            await Entities.AddRangeAsync(entities);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id, bool saveChanges = true)
        {
            var entity =  await Entities.FindAsync(id);
            await DeleteAsync(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            Entities.Remove(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }

        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                Entities.RemoveRange(enumerable);
            }
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await Entities.AnyAsync(expression);
        }
    }
}
