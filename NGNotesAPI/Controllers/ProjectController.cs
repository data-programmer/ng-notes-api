using System.Threading.Tasks;
using NGNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NGNotesAPI.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace NGNotesAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetAllProjects/{UserId}")]
        public async Task<ActionResult<List<ProjectModel>>> GetAllProjects(int UserId)
        {
            var Projects = await _projectService.GetAllProjectsAsync(UserId);
            if (Projects == null) return NotFound();

            return Ok(Projects);
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult<ProjectModel>> CreateProject([FromBody] ProjectModel Project)
        {
            var NewProject = await _projectService.CreateProjectAsync(Project);
            if (NewProject == null) return NotFound();

            return Ok(NewProject);
        }

        [HttpPut("UpdateProject")]
        public ActionResult<ProjectModel> UpdateProject([FromBody] ProjectModel Project)
        {
            var UpdatedProject = _projectService.UpdateProject(Project);
            if (UpdatedProject == null) return NotFound();

            return Ok(UpdatedProject);
        }

        [HttpDelete("DeleteProject/{ProjectId}")]
        public ActionResult<ProjectModel> DeleteProject(int ProjectId)
        {
            var DeleteProject = _projectService.DeleteProject(ProjectId);
            if (DeleteProject == null) return NotFound();

            return Ok(DeleteProject);
        }
    }
}