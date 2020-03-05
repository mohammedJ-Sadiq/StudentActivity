﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentActivity.Models;

namespace StudentActivity.ViewModel
{
    public class StudentProgram
    {
        public Student Student { get; set; }
        public IEnumerable<Program> Program { get; set; }
    }
}