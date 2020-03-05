using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Program
    {
        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }

        [Required]
        public string MaximumStudentNumber { get; set; }
        public string AdminName { get; set; }
        public string EvaluationType { get; set; }

        public string CertifiactionType { get; set; }

        public Club Club { get; set; }

        [Required]
        public int ClubId { get; set; }

        public IList<Student_Program> StudentPrograms { get; set; }

    }
}