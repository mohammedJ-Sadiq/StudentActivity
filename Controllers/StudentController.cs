using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;

namespace StudentActivity.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var students = GetStudent();

            return View(students);
        }

        private IEnumerable<Student> GetStudent()
        {
            return new List<Student>
            {
                new Student{ Id = 36110580 , Name = "Mohammed j. Sadiq", MobileNo = "0547158828",Email = "diamoh0@gmail.com"},
                new Student{ Id = 36110379 , Name = "Ahmed m. Alsaleh" , MobileNo = "0557724451", Email = "ahmad.amadiii@gmail.com"},
                new Student{ Id = 36115552 , Name = "Marwan h. talody" , MobileNo = "0544442285", Email = "marwanOrg1@yahoo.com"}
            };
        }
    }
}