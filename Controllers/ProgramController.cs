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

            return View();
        }
        
        public ActionResult Registration()
        {
            var Programs = _context.Programs.ToList();
            var viewModels = new StudentProgram()
            {
                StudentPrograms = new Student_Program(),
                Program = Programs
            };
            
            return View("RegistrationForm", viewModels);
        }

        [HttpPost]
        
        public ActionResult Save(StudentProgram studentProgram)
        {


            
            _context.StudentPrograms.Add(studentProgram.StudentPrograms);

            _context.SaveChanges();
            
                
            
            return RedirectToAction("Index", "Program");
        }

        public ActionResult addProgram()
        {
            var clubs = _context.Clubs.ToList();
            var viewModels = new ProgramViewModel()
            {
                program = new Program(),
                club = clubs          
            };
            

            return View("ProgramForm",viewModels);
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