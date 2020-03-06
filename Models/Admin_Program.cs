using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentActivity.Models
{
    public class Admin_Program
    {
        public Admin Admin { get; set; }

        [Required]
        public string AdminId { get; set; }

        public Program Program { get; set; }

        [Required]
        public int ProgramId { get; set; }
    }
}