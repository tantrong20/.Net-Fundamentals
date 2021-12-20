using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task CreateProject(Project project)
        {
            await _projectService.Create(project);
        }

        [HttpPost("{id}")]
        public async Task AddProjectMember(int projectId, int userId)
        {
            await _projectService.AddProjectMember(projectId, userId);
        }

        [HttpPut("{id}")]
        public async Task UpdateProjectName(int id, Project project)
        {
            await _projectService.UpdateProjectName(id, project);
        }
    }
}
