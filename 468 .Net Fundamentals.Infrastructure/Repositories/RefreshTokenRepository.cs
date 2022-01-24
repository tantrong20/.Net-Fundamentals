using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {

    }
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}

