using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            var studentProgram = new Student_Program();
            return View("RegistrationForm");
        }

        [HttpPost]
        
        public ActionResult Save(Student_Program studentProgram)
        {


            
            _context.StudentPrograms.Add(studentProgram);

            _context.SaveChanges();
            
                
            
            return RedirectToAction("Index", "Program");
        }

        public ActionResult addProgram()
        {
            
            var viewModels = new ProgramViewModel()
            {
                program = new Program(),
                
            };
            

            return View("ProgramForm");
        }

        [HttpPost]
        public ActionResult SavePrg(Program program)
        {



            _context.Programs.Add(program);


            _context.SaveChanges();

            return RedirectToAction("Index", "Program");
        }

    }
}