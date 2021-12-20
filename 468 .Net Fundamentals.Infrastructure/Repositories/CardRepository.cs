using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
       
    }

    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {

        public CardRepository(ApplicationDbContext context) : base(context)
        {

        }
     
    }
}
