using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentActivity.Models;

namespace StudentActivity.ViewModel
{
    public class ProgramViewModel
    {
        public Program program { get; set; }
        public IEnumerable<Club> club { get; set; }
    }
}