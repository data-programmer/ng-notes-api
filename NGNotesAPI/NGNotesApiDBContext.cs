using NGNotesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace NGNotesAPI
{
    public class NGNotesApiDBContext : DbContext
    {
        public NGNotesApiDBContext(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> User { get; set; }

        public DbSet<ProjectModel> Project { get; set; }

        public DbSet<NoteModel> Note { get; set; }
    }
}
