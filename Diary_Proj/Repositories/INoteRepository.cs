using Diary_Proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Proj.Repositories
{
    /// <summary>
    /// This Repository resposible for managing data in the Datastore (DB)
    /// </summary>
    public interface INoteRepository
    {
        /// <summary>
        /// Add <code>Note</code> To the Data store
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        Task AddNote( Note note);

        /// <summary>
        /// Retrive Note form the Data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Note</returns>
        Task<Note> GetNote(int id);

        /// <summary>
        /// Get all notes in the spacified <code>Day</code> 
        /// </summary>
        /// <param name="date">Date which we want to retrive all notes attached to it</param>
        /// <returns>A collection of Notes on a single day</returns>
        ICollection<Note> GetNotesOfDay(DateTime date);
    }
}
