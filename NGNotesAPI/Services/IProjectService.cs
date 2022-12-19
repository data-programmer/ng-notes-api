using System.Threading.Tasks;
using System.Collections.Generic;
using NGNotesAPI.Models;

namespace NGNotesAPI.Services
{
    public interface IProjectService
    {
        public Task<List<ProjectModel>> GetAllProjectsAsync(int UserId);

        public Task<ProjectModel> CreateProjectAsync(ProjectModel project);

        public ProjectModel UpdateProject(ProjectModel project);

        public ProjectModel DeleteProject(int ProjectId);
    }
}
