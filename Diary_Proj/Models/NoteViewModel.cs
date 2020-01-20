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

        [MaxLength(70, ErrorMessage = "Legnth Must be less than 70 char")]
        public String Title { get; set; }

        public String Text { get; set; }

        public TimeSpan? StartAt { get; set; }

        public String PicPath { get; set; }
        public DateTime Date_FK { get; set; }
        public IFormFile Pic { get; set; }
    }
}
