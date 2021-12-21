using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task Create([FromBody] ProjectCreateRequest projectCreateVM)
        {
            await _projectService.Create(projectCreateVM);
        }

        [HttpGet]
        public async Task<IList<Project>> GetAll()
        {
            return await _projectService.GetAll();
        }

        [HttpGet("id")]
        public async Task<ProjectDetailsVM> Get(int id)
        {
            return await _projectService.Get(id);
        }

        [HttpPut("id")]
        public async Task Update(int id, [FromBody] string name)
        {
            await _projectService.Update(id, name);
        }

        [HttpDelete("id")]
        public async Task Delete(int id)
        {
            await _projectService.Delete(id);
        }

        // Business

        [Route("id/Business")]
        [HttpPost]
        public async Task CreateBusiness(int id, [FromBody] BusinessCreateRequest request)
        {
            await _projectService.CreateBusiness(id, request);
        }

        [Route("id/Business/busId")]
        [HttpPut]
        public async Task UpdateBusiness(int busId, [FromBody] string name)
        {
            await _projectService.UpdateBusiness(busId, name);
        }

        [Route("id/Business/busId")]
        [HttpDelete]
        public async Task DeleteBusiness(int busId)
        {
            await _projectService.DeleteBusiness(busId);
        }


    }
}
