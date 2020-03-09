using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentActivity.Models;

namespace StudentActivity.ViewModel
{
    public class StudentProgram
    {
        public Student_Program StudentPrograms { get; set; }
        public IEnumerable<Program> Program { get; set; }

    }
}