using AutoMapper;
using NGNotesAPI.Models;

namespace NGNotesAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NoteModel, NoteModel>();
            CreateMap<ProjectModel, ProjectModel>();
            CreateMap<UserModel, UserModel>();
        }
    }
}
