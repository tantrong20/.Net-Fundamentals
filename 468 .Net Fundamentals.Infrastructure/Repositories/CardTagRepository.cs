using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface ICardTagRepository : IRepository<CardTag>
    {

    }
    public class CardTagRepository : RepositoryBase<CardTag>, ICardTagRepository
    {
        public CardTagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
