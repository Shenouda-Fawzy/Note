using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Proj.Models
{
    public class NoteViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Legnth Must be less than 70 char")]
        public String Title { get; set; }

        public String Text { get; set; } = String.Empty;

        public TimeSpan? StartAt { get; set; } = default;

        public String PicPath { get; set; } = "";

        [Required(ErrorMessage = "Please Pick the note date")]
        public DateTime Date_FK { get; set; } = default;
        public IFormFile Pic { get; set; }
    }
}
