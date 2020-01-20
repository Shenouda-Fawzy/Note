using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Proj.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }

        public String Title { get; set; }

        public String Text { get; set; }

        public TimeSpan? StartAt { get; set; }

        public String Pic { get; set; }

        [ForeignKey("DayNote")]
        [Column(name:"Date", TypeName = "DATE")]
        public DateTime Date_FK { get; set; }
        // IT will be mapped to FK
        public DayNote DayNote { get; set; }
    }
}
