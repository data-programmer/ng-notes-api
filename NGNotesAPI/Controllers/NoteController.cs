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
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("GetAllRecentNotes/{UserId}")]
        public async Task<ActionResult<List<NoteModel>>> GetAllRecentNotes(int UserId)
        {
            var RecentNotes = await _noteService.GetAllRecentNotesAsync(UserId);
            if (RecentNotes == null) return NotFound();

            return Ok(RecentNotes);
        }

        [HttpGet("GetAllNotes/{UserId}&{ProjectId}")]
        public async Task<ActionResult<List<NoteModel>>> GetAllNotes(int UserId, int ProjectId)
        {
            var Notes = await _noteService.GetAllNotesAsync(UserId, ProjectId);
            if (Notes == null) return NotFound();

            return Ok(Notes);
        }

        [HttpPost("CreateNote")]
        public async Task<ActionResult<NoteModel>> CreateNote([FromBody] NoteModel Note)
        {
            var NewNote = await _noteService.CreateNoteAsync(Note);
            if (NewNote == null) return NotFound();

            return Ok(NewNote);
        }

        [HttpPut("UpdateNote")]
        public ActionResult<NoteModel> UpdateNote([FromBody] NoteModel Note)
        {
            var UpdateNote = _noteService.UpdateNote(Note);
            if (UpdateNote == null) return NotFound();

            return Ok(UpdateNote);
        }

        [HttpDelete("DeleteNote/{NoteId}")]
        public ActionResult<NoteModel> DeleteNote(int NoteId)
        {
            var DeleteNote = _noteService.DeleteNote(NoteId);
            if (DeleteNote == null) return NotFound();

            return Ok(DeleteNote);
        }
    }
}