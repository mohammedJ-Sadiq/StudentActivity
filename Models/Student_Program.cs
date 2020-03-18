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
        [Display(Name = "Student Id")]
        public string StudentId { get; set; }
        
        public Program Program { get; set; }

        [Required]
        [Display(Name = "Program Title")]
        public int ProgramId { get; set; }

        [Display(Name = "Eligible")]

        public bool IsEligible { get; set; }
        public List<bool> Eligible { get; set; }

        public Student_Program()
        {
            
        }

        public Student_Program(int id)
        {
            ProgramId = id;
        }

        public Student_Program(int programId ,string studentId)
        {
            ProgramId = programId;
            StudentId = studentId;

        }
    }
}