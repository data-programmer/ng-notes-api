using System.Threading.Tasks;
using System.Collections.Generic;
using NGNotesAPI.Models;

namespace NGNotesAPI.Services
{
    public interface INoteService
    {
        public Task<List<NoteModel>> GetAllRecentNotesAsync(int UserId);

        public Task<List<NoteModel>> GetAllNotesAsync(int UserId, int ProjectId);

        public Task<NoteModel> CreateNoteAsync(NoteModel note);

        public NoteModel UpdateNote(NoteModel note);

        public NoteModel DeleteNote(int NoteId);
    }
}
