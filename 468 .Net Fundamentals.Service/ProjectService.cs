using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrrentUser _currrentUser;

        public ProjectService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ICurrrentUser currrentUser)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _currrentUser = currrentUser;
        }

        public async Task Create(string name)
        {
            try
            {
                var currentUserId = _currrentUser?.Id;

                var project = new Project()
                {
                    CreatedBy = currentUserId,
                    Name = name
                };

                await _unitOfWork.Repository<Project>().InsertAsync(project);
                                
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<ProjectVM> Get(int id)
        {
            try
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
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<IList<ProjectVM>> GetAll()
        {
            try
            {
                /* var currentUserId = _currrentUser?.Id;*/

                var projectVMs = await _unitOfWork.Repository<Project>()
                    .Query()
                    /*.Where(_ => _.CreatedBy == currentUserId)*/
                    .Select(project => new ProjectVM
                    {
                        Id = project.Id,
                        Name = project.Name,
                        CreatedBy = project.CreatedBy,
                    })
                    .ToListAsync();
                return projectVMs;
            }
            catch (Exception e)
            {

                throw e;
            }

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
                throw e;
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
                throw e;
            }
        }
    }
}
