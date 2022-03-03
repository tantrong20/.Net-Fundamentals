using _468_.Net_Fundamentals.Controllers.ActionFilters;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    /*[Authorize(Roles = AppUserRole.Admin)]*/
    public class ProjectsController : ControllerBase
    {
        private IProjectService _projectService;
        private readonly ILogger<ProjectsController> _logger;
        public ProjectsController(IProjectService projectService, ILogger<ProjectsController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Permissions.Projects.Create)]
        public async Task Create([FromBody] string name)    
        {
            await _projectService.Create(name);
        }

        [HttpGet]
        public async Task<IList<ProjectVM>> GetAll()
        {
            _logger.LogInformation("Hello from logger...");
            return await _projectService.GetAll();
        }

        /*[LogActionFilter]*/
        /*[TypeFilter(typeof(ProjectMemberActionFilter))]*/
        [HttpGet("{id}")]
        public async Task<ProjectVM> Get(int id)
        {
            return await _projectService.Get(id);
        }

        [HttpPut("{id}")]
        [Authorize(Permissions.Projects.Edit)]
        public async Task Update(int id, [FromBody] string name)
        {
            await _projectService.Update(id, name);
        }

        [HttpDelete("{id}")]
        [Authorize(Permissions.Projects.Delete)]
        public async Task Delete(int id)
        {
            await _projectService.Delete(id);
        }

        
    }
}
