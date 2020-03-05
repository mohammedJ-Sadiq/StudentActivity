using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Student_Program
    {
        
        public Student Student { get; set; }
        [Required]
        public string StudentId { get; set; }
        
        public Program Program { get; set; }
        [Required]
        public int ProgramId { get; set; }
    }
}