using AutoMapper;
using NGNotesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace NGNotesAPI.Services
{
    public class DefaultNoteService : INoteService
    {
        private readonly NGNotesApiDBContext _context;
        private readonly IMapper _mapper;

        public DefaultNoteService(
            NGNotesApiDBContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<NoteModel>> GetAllRecentNotesAsync(int UserId)
        { 
            // Change to grab recently created notes in the future
            var entity = await _context.Note
                .FromSqlRaw("select top 6 * from [dbo].[Note] where UserId = " + UserId + " order by Id desc")
                .Where(x => x.UserId == UserId)
                .ToListAsync();

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<List<NoteModel>>(entity);
        }

        public async Task<List<NoteModel>> GetAllNotesAsync(int UserId, int ProjectId)
        {
            var entity = await _context.Note
                .Where(x => x.UserId == UserId & x.ProjectId == ProjectId)
                .ToListAsync();

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<List<NoteModel>>(entity);
        }


        public async Task<NoteModel> CreateNoteAsync(NoteModel note)
        {
            var entity = await _context.Note.AddAsync(note);

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<NoteModel>(entity.Entity);
        }

        public NoteModel UpdateNote(NoteModel note)
        {
            var entity = _context.Note
                .Update(note);

            if (entity == null)
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<NoteModel>(entity.Entity);
        }

        public NoteModel DeleteNote(int NoteId)
        {
            var entity = _context.Note
                .Where(x => x.Id == NoteId)
                .SingleOrDefault();

            var result = _context.Note.Remove(entity);

            if (result == null)
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<NoteModel>(result.Entity);
        }        
    }
}