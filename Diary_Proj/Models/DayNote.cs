using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Proj.Models
{
    public class DayNote
    {
        public DayNote()
        {
            Notes = new HashSet<Note>();
        }

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
