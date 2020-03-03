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

        public String Id { get; set; }
        
        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Email { get; set; }

        public IList<Program> Programs { get; set; }

    }
}