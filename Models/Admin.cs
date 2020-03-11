using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Admin {
        [StringLength(14)]
        public string Id { get; set; }

        
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        public IList<Admin_Program> AdminProgram { get; set; }



    }
}