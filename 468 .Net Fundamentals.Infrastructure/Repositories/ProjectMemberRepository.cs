using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface IProjectMemberRepository : IRepository<ProjectMember>
    {
       
    }
    public class ProjectMemberRepository : RepositoryBase<ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
