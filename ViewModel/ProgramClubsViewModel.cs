using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentActivity.Models;

namespace StudentActivity.ViewModel
{
    public class ProgramClubsViewModel
    {
        public IEnumerable<Program> Programs { get; set; }
       
        public IEnumerable<Club> Clubs { get; set; }

    }
}