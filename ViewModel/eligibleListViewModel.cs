using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentActivity.Models;

namespace StudentActivity.ViewModel
{
    public class eligibleListViewModel
    {
        public IEnumerable<Student>  Student { get; set; }
    }
}