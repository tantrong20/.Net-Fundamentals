using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task Create([FromBody] Project project)
        {
            await _projectService.Create(project);
        }

        [HttpGet]
        public async Task<IList<Project>> GetAll()
        {
            return await _projectService.GetAll();
        }

        [HttpGet("id")]
        public async Task<Project> Get(int id)
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
    }
}
