using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> Create(string name)
        {
            try
            {
                /*await _unitOfWork.BeginTransaction();*/

                var userLogin = await _unitOfWork.Repository<User>().FindAsync(1);

                var project = new Project
                {
                    Name = name,
                    CreatedBy = userLogin.Id,
                };

                await _unitOfWork.Repository<Project>().InsertAsync(project);
                await _unitOfWork.SaveChangesAsync();

                return 1;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                return 0;
            }
        }

        public async Task<ProjectVM> Get(int id)
        {
            var project = await _unitOfWork.Repository<Project>().FindAsync(id);
       
            var projectVM = new ProjectVM
            {
                Id = project.Id,
                Name = project.Name,
                CreatedBy = project.CreatedBy,
            };

            return projectVM;
        }

        public async Task<IList<ProjectVM>> GetAll()
        {
            var projectVMs = await _unitOfWork.Repository<Project>()
                        .Query()
                        .Where(_ => _.CreatedBy == 1)
                        .Select(project => new ProjectVM
                        {
                            Id = project.Id,
                            Name = project.Name,
                            CreatedBy = project.CreatedBy
                        })
                        .ToListAsync();
            return projectVMs;
        }

        public async Task Update(int id, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var project = await _unitOfWork.Repository<Project>().FindAsync(id);
                project.Name = name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.Repository<Project>().DeleteAsync(id);
              
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }          
    }
}
