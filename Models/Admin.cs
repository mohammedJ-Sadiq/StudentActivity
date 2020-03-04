using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Admin {
        public string Id { get; set; }

        
        [Required]
        public string Name { get; set; }

    }
}