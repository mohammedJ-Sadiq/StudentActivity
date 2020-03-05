using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;
using StudentActivity.ViewModel;

namespace StudentActivity.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program
        private ApplicationDbContext _context;

        public ProgramController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var programs = _context.Programs.Include(p =>p.Club).ToList();

            return View(programs);
        }
        
        public ActionResult Registration()
        {
            var programs = _context.Programs.ToList();
            var viewModels = new StudentProgram()
            {
                Student = new Student(),
                Program = programs

            };
            return View("RegistrationForm", viewModels);
        }
    }
}