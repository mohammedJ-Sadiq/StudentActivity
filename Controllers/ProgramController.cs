using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    // ADMIN ACTIONS   
        public ActionResult Index()
        {
            var programs = _context.Programs.Include(p =>p.Club).ToList();

            return View(programs);
        }

        public ActionResult RegisteredPrograms()
        {
            var eligList = _context.Programs.Include(c => c.Club).ToList();

            return View(eligList);
        }

        public ActionResult EligibleList(int id)
        {
            var students = _context.StudentPrograms.Include(s => s.Student).Where(p => p.ProgramId == id);
            return View(students);
        }

        // To let the student register for a program
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

        // To add new student registered manually from the admin
        public ActionResult RegistrationFromAdmin(int id)
        {
            var StudentPrograms = new Student_Program(id);
            
            return View("RegistrationFormAdmin", StudentPrograms);
        }

        // To save new student register for a program
        [HttpPost]
        public ActionResult Save(StudentProgram studentProgram)
        {
            if (!ModelState.IsValid)
            {
                var viewModels = new StudentProgram()
                {
                    StudentPrograms = new Student_Program(),
                    Program = _context.Programs.ToList()
                };

                return View("RegistrationForm", viewModels);
            }
            
            _context.StudentPrograms.Add(studentProgram.StudentPrograms);

            _context.SaveChanges();
            
            return RedirectToAction("Index", "Program");
        }

        // To save added to student_program table from the admin
        public ActionResult SaveFromAdmin(Student_Program studentProgram)
        {
            if (!ModelState.IsValid)
            {
                var StudentPrograms = new Student_Program(studentProgram.ProgramId);

                return View("RegistrationFormAdmin", StudentPrograms);
            }
            _context.StudentPrograms.Add(studentProgram);

            _context.SaveChanges();

            return RedirectToAction("EligibleList", "Program", new {id = studentProgram.ProgramId });
        }

        // To add a new program 
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

        // To save the added program
        [HttpPost]
        public ActionResult SavePrg(Program program)
        {
            if (!ModelState.IsValid)
            {
                var viewModels = new ProgramViewModel()
                {
                    program = new Program(),
                    club = _context.Clubs.ToList()
                };

                return View("ProgramForm", viewModels);
            }
            if(program.Id == 0)
            {
                _context.Programs.Add(program);
            }
            else
            {
                var programInDb = _context.Programs.Single(p => p.Id == program.Id);
                programInDb.Title = program.Title;
                programInDb.Time = program.Time;
                programInDb.StartDate = program.StartDate;
                programInDb.EndDate = program.EndDate;
                programInDb.MaximumStudentNumber = program.MaximumStudentNumber;
                programInDb.ClubId = program.ClubId;
                programInDb.Description = program.Description;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Program");
        }

        public ActionResult EditProgram(int id)
        {
            var program = _context.Programs.SingleOrDefault(p => p.Id == id);
            if (program == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProgramViewModel
            {
                program = program,
                club = _context.Clubs.ToList()
            };
            return View("ProgramForm", viewModel);
        }

        public ActionResult ProgramDetailsAdmin(int id)
        {
            var program = _context.Programs.Include(c => c.Club).SingleOrDefault(p => p.Id == id);
            return View(program);
        }

    // END OF ADMIN ACTIONS
    
    // STUDENT ACTIONS
        // To show registered programs for student
        public ActionResult StuPrograms(string id)
        {
            var programs = _context.StudentPrograms.Include(p => p.Program).Include(c => c.Program.Club).Where(s => s.StudentId == id);
            return View(programs);
        }

        public ActionResult ProgramDetailsStudent(int id)
        {
            var program = _context.Programs.Include(c => c.Club).SingleOrDefault(p => p.Id == id);
            return View(program);
        }
    
    // END OF STUDENT ACTION

    }
}