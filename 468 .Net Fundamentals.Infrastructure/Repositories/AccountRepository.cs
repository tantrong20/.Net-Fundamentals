using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface IAccountRepository : IRepository<AppUser>{
        
    }
    public class AccountRepository : RepositoryBase<AppUser>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
