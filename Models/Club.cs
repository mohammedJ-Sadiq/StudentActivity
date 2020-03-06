using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Club
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Club Name")]
        public string Name { get; set; }

        public Student CoordinatorName { get; set; }
        
        [Required]
        public String StudentId { get; set; }

        public virtual ICollection<Program> Programs { get; set; }

        public IList<Student_Club> StudentClub { get; set; }
    }
}