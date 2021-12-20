using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface ICardAssignRepository : IRepository<CardAssign>
    {

    }
    public class CardAssignRepository : RepositoryBase<CardAssign>, ICardAssignRepository
    {
        public CardAssignRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
