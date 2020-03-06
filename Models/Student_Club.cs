using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Student_Club
    {
        public Student Student { get; set; }

        [Required]
        public string StudentId { get; set; }

        public Club Club { get; set; }

        [Required]
        public int ClubId { get; set; }

    }
}