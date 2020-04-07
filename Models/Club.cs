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
        [StringLength(60)]
        public string Name { get; set; }

        public Student Student { get; set; }
        
        [Required]
        [Display(Name ="Club Coordinator")]
        public String StudentId { get; set; }

        [Display (Name ="Club Vision")]
        [StringLength(1000)]
        public String ClubVision { get; set; }

        public virtual ICollection<Program> Programs { get; set; }

        public IList<Student_Club> StudentClub { get; set; }
    }
}