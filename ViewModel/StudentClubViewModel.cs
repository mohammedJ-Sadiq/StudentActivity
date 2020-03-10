using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentActivity.Models;

namespace StudentActivity.ViewModel
{
    public class StudentClubViewModel
    {
        public Student_Club StudentClub { get; set; }
       
        public IEnumerable<Club> Clubs { get; set; }

    }
}