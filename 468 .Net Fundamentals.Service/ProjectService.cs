using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class ProjectService : RepositoryBase<Project>, IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProjectMember(int projectId, int useId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var project = await _unitOfWork.Repository<Project>().FindAsync(projectId);
                var user = await _unitOfWork.Repository<User>().FindAsync(useId);


                var projectMember = new ProjectMember
                {
                    Project = project,
                    User = user,
                    AddOn = DateTime.Now
                };

                await _unitOfWork.Repository<ProjectMember>().InsertAsync(projectMember);
            
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }


        public async Task Create(Project project)
        {
            try
            {
                await _unitOfWork.BeginTransaction();


                var user = await _unitOfWork.Repository<User>().FindAsync(1);
                project.User = user;


                await _unitOfWork.Repository<Project>().InsertAsync(project);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }



        public async Task UpdateProjectName(int id, Project projecNew)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var project = await _unitOfWork.Repository<Project>().FindAsync(id);

                project.Name = projecNew.Name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
    }
}
