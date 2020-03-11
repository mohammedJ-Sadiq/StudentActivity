using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace StudentActivity.Models
{
    public class Student
    {
        
        [StringLength(14)]
        [Required]
        public String Id { get; set; }
        
        [Required]
        [Display(Name = "Student Name")]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }
        public IList<Student_Program> StudentPrograms { get; set; }

        public IList<Student_Club> StudentClub { get; set; }

        


    }
}