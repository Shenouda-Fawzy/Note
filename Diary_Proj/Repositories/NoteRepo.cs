using Diary_Proj.Data;
using Diary_Proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Proj.Repositories
{
    public class NoteRepo : INoteRepository
    {
        private readonly AppDbContext _context;
        public NoteRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddNote(Note note)
        {
            if (IsDayExsts(note.Date_FK))
            {
                await _context.Notes.AddAsync(note);
            }
            else
            {
                DayNote dayNote = new DayNote { Date = note.Date_FK.Date };
                dayNote.Notes.Add(note);
                await _context.AddAsync(dayNote);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Note> GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Note> GetNotesOfDay(DateTime date)
        {
            var result = _context.Notes.Where(d => d.Date_FK.Date == date.Date).OrderBy(n => n.StartAt).ToList();
            //var result = _context.Notes.GroupBy(g => g.Date_FK).OrderBy(n => n.StartAt).ToList();
            return result;
        }

        public  bool IsDayExsts(DateTime date)
        {
            return  _context.DayNotes.Any(d => d.Date.Date == date.Date);
        }
    }
}
