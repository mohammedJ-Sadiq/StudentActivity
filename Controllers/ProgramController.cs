using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;

namespace StudentActivity.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program
        public ActionResult Index()
        {
            var programs = GetProgram();

            return View(programs);
        }
        private IEnumerable<Program> GetProgram()
        {
            return new List<Program>
            {
                new Program{Id = 1, Title = "Cyber Security",Time = "9:00",
                    StartDate = "12-3-2020", EndDate = "14-3-2020",
                    MaximumStudentNumber = "30", AdminName = "Rashed Alqhtani",
                    CertifiactionType = "Attendance", ClubName = "Computer Sceince",
                    EvaluationType = "Program"
                },

                new Program{Id = 2, Title = "Photo Shooting",Time = "1:00",
                    StartDate = "20-3-2020", EndDate = "25-3-2020",
                    MaximumStudentNumber = "15", AdminName = "Yousef Alghamdi",
                    CertifiactionType = "Voluntarily", ClubName = "Photography",
                    EvaluationType = "Activity"
                }



            };
        }
    }
}