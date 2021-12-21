using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;


namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface IBusinessRepository : IRepository<Business>
    {

    }
    public class BusinessRepository : RepositoryBase<Business>, IBusinessRepository
    {
        public BusinessRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
