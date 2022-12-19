using AutoMapper;
using NGNotesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace NGNotesAPI.Services
{
    public class DefaultProjectService : IProjectService
    {
        private readonly NGNotesApiDBContext _context;
        private readonly IMapper _mapper;

        public DefaultProjectService(
            NGNotesApiDBContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProjectModel>> GetAllProjectsAsync(int UserId)
        {
            var entity = await _context.Project
                .Where(x => x.UserId == UserId)
                .ToListAsync();

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<List<ProjectModel>>(entity);
        }

        public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
        {
            var entity = await _context.Project.AddAsync(project);

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectModel>(entity.Entity);
        }

        public ProjectModel UpdateProject(ProjectModel project)
        {
            var entity = _context.Project
                .Update(project);

            if (entity == null)
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<ProjectModel>(entity.Entity);
        }

        public ProjectModel DeleteProject(int ProjectId)
        {
            var projectEntity = _context.Project
                .Where(x => x.Id == ProjectId)
                .SingleOrDefault();

            var projectNotes = _context.Note
                .Where(x => x.ProjectId == projectEntity.Id)
                .ToList();

            foreach (NoteModel note in projectNotes)
            {
                _context.Note.Remove(note);
            }

            var result = _context.Project.Remove(projectEntity);

            if (result == null)
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<ProjectModel>(result.Entity);
        }
    }
}