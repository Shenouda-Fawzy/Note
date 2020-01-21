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

// Get Unique name to store it in DB
                string uniqName = string.Empty;
                uniqName = await UploadImage(noteModel);
                noteModel.PicPath = uniqName;


                Note note = CopyPOCOFromVM(noteModel);
                
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

            NoteViewModel model = CopyViewModelFromPOCO(note);

            return View(model);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NoteViewModel noteViewModel)
        {
            if (id != noteViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!IsDayExsts(noteViewModel.Date_FK.Date))
                    {
                        DayNote dayNote = new DayNote();
                        dayNote.Date = noteViewModel.Date_FK;
                        _context.DayNotes.Add(dayNote);
                    }
// Get Unique name to store it in DB
                    string uniqName = string.Empty;
                    uniqName = await UploadImage(noteViewModel);
                    noteViewModel.PicPath = uniqName;

                    Note note = CopyPOCOFromVM(noteViewModel);

                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(noteViewModel.ID))
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
            return View(noteViewModel);
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

        private Note CopyPOCOFromVM(NoteViewModel vm) 
        {
            Note note = new Note()
            {
                ID = vm.ID,
                Date_FK = vm.Date_FK.Date,
                Title = vm.Title,
                Text = vm.Text,
                StartAt = vm.StartAt,
                Pic = vm.PicPath
            };
            return note;
        }

        private NoteViewModel CopyViewModelFromPOCO(Note note) 
        {
            NoteViewModel model = new NoteViewModel
            {
                ID = note.ID,
                Date_FK = note.Date_FK,
                PicPath = note.Pic,
                StartAt = note.StartAt,
                Text = note.Text,
                Title = note.Title
            };
            return model;
        }
    }
}
