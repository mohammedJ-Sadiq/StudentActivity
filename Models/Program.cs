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
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        [StringLength(8)]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [StringLength(10)]
        public string StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [StringLength(10)]
        public string EndDate { get; set; }

        [Required]
        [Display(Name = "Maximum Students Number")]
        public int MaximumStudentNumber { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public string AdminName { get; set; }
        public string EvaluationType { get; set; }

        public string CertifiactionType { get; set; }

        public Club Club { get; set; }

        [Required]
        [Display(Name = "Club Name")]
        public int ClubId { get; set; }

        // for programs deleted
        public bool IsDeleted { get; set; }

        // for programs requested by club coordinator
        public bool IsVisible { get; set; }

        public IList<Student_Program> StudentPrograms { get; set; }

        public IList<Admin_Program> AdminProgram { get; set; }

    }
}