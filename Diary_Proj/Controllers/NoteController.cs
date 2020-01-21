using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diary_Proj.Data;
using Diary_Proj.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Diary_Proj.Repositories;

namespace Diary_Proj.Controllers
{
    public class NoteController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly INoteRepository _noteRepository;

        public NoteController(AppDbContext context, IWebHostEnvironment appEnvironment, INoteRepository noteRepository)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _noteRepository = noteRepository;
        }

        // GET: Note
        public async Task<IActionResult> Index()
        {
            List<DayNote> dayNote = _context.DayNotes.OrderBy(d => d.Date).ToList();
            List<Note> notes = new List<Note>();
            foreach (var day in dayNote)
            {
                notes.AddRange(_noteRepository.GetNotesOfDay(day.Date));
            }

            //var allNotes = _noteRepository.GetNotesOfDay(new DateTime(2021, 2, 2));
            return View(notes);
        }

        // GET: Note/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Note/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteViewModel noteModel)
        {
            if (ModelState.IsValid)
            {
                string uniqName = string.Empty;
                uniqName = await UploadImage(noteModel);

                Note note = new Note
                {
                    ID = noteModel.ID,
                    Date_FK = noteModel.Date_FK.Date,
                    Title = noteModel.Title,
                    Text = noteModel.Text,
                    StartAt = noteModel.StartAt,
                    Pic = uniqName
                };

                await _noteRepository.AddNote(note);
                return RedirectToAction(nameof(Index));
            }
            return View(noteModel);
        }

        private async Task<string> UploadImage(NoteViewModel noteModel)
        {
            string uniqName = string.Empty;
            if (noteModel.Pic != null)
            {
                var uploadFolderPath = Path.Combine(_appEnvironment.WebRootPath, "images");

// Create Unique name to ensure that no conflict will happen if the same image is upladed again
                uniqName = Guid.NewGuid().ToString() + "_" + noteModel.Pic.FileName;
                var filePath = Path.Combine(uploadFolderPath, uniqName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await noteModel.Pic.CopyToAsync(fileStream);
                }
            }

            return uniqName;
        }

        // GET: Note/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Text,StartAt,Pic")] Note note)
        {
            if (id != note.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Note/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.ID == id);
        }

        private bool IsDayExsts(DateTime date) 
        {
            return _context.DayNotes.Any(d => d.Date.Date == date.Date);
        }
    }
}
