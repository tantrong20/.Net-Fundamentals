using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {

    }
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
