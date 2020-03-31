using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using StudentActivity.Models;
using StudentActivity.ViewModel;

namespace StudentActivity.Controllers
{
    public class ArabicProgramController : Controller
    {
        // GET: Program
        private ApplicationDbContext _context;

        // Attributes of delete pop warning message box 
        string DeleteMsgContent = StudentActivity.Resources.Language.Delete_Program_Confirmation;
        string DeleteMsgTitle = StudentActivity.Resources.Language.Delete_Program;
        MessageBoxButtons DeleteMsgButtons = MessageBoxButtons.YesNo;

        public ArabicProgramController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    // ADMIN ACTIONS   

        [Authorize(Roles ="CanManagePrograms")]
        public ActionResult Index(string language)
        {

            ChangeLanguageFunction(language);

            var programs = _context.Programs.Include(p =>p.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);

            return View(programs);
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RegisteredPrograms(string language)
        {

            ChangeLanguageFunction(language);

            var eligList = _context.Programs.Include(c => c.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);
            return View(eligList);
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EligibleList(int id, string language)
        {

            ChangeLanguageFunction(language);

            var students = _context.StudentPrograms.Include(s => s.Student).Where(p => p.ProgramId == id);
            return View(students);
        }



        // To add new student registered manually from the admin

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RegistrationFromAdmin(int id, string language)
        {

            ChangeLanguageFunction(language);

            var StudentPrograms = new Student_Program(id);
            
            return View("RegistrationFormAdmin", StudentPrograms);
        }

        // To save added to student_program table from the admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SaveFromAdmin(Student_Program studentProgram, string language)
        {

            ChangeLanguageFunction(language);

            if (!ModelState.IsValid)
            {
                var StudentPrograms = new Student_Program(studentProgram.ProgramId);

                return View("RegistrationFormAdmin", StudentPrograms);
            }
            _context.StudentPrograms.Add(studentProgram);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Added_student_already_registerd_in_program +" "+ StudentActivity.Resources.Language.Student_not_registerd_in_website, "Existence Error");
            }

            return RedirectToAction("EligibleList", "ArabicProgram", new {id = studentProgram.ProgramId });
        }

        // To add a new program 
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult addProgram(string language)
        {

            ChangeLanguageFunction(language);

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SavePrg(Program program, string language)
        {

            ChangeLanguageFunction(language);

            if (!ModelState.IsValid)
            {
                var viewModels = new ProgramViewModel()
                {
                    program = new Program(),
                    club = _context.Clubs.ToList()
                };

                return View("ProgramForm", viewModels);
            }
            // To retrieve a program
            
            if(program.Id == 0)
                _context.Programs.Add(program);
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

            /* This exception will never occur because program id
               is auto generated by the database */
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Program_already_added, "Existence Error");
            }
            return RedirectToAction("Index", "ArabicProgram");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EditProgram(int id, string language)
        {

            ChangeLanguageFunction(language);

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

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult DeleteProgram(int id, string language)
        {

            ChangeLanguageFunction(language);

            var program = _context.Programs.Single(p => p.Id == id);

            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                // If IsDeleted set true then it is simply deleted in the view
                program.IsDeleted = true;
                _context.SaveChanges();
            } 

            return RedirectToAction("Index", "ArabicProgram");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RetrievePrg(string language)
        {

            ChangeLanguageFunction(language);

            return View();
        }

        // To save the program retrieve by admin
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SaveRetrievePrg(Program program, string language)
        {

            ChangeLanguageFunction(language);

            var flag1 = false;
            var flag2 = false;
            foreach (var prg in _context.Programs)
            {
                if (program.Title == prg.Title && prg.IsDeleted == true)
                {
                    prg.IsDeleted = false;
                    flag1 = true;
                }

                else if (program.Title == prg.Title && prg.IsDeleted == false)
                    flag2 = true;
            }

            if (flag1 == false && flag2 == false)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Retrieve_program_not_exist, "Existence Error");
            }
            else if (flag2 == true)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Retrieve_program_already_available, "Existence Error");
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "ArabicProgram");
        }

        [Authorize(Roles = "CanManagePrograms,CanManageClubs" )]
        public ActionResult RequestedPrograms(string language)
        {

            ChangeLanguageFunction(language);

            var programs = _context.Programs.Include(p => p.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == false);

            return View(programs);
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult ApproveProgram(int id, string language)
        {

            ChangeLanguageFunction(language);

            var program = _context.Programs.Single(p => p.Id == id);

            DialogResult result = MessageBox.Show(StudentActivity.Resources.Language.Approve_program_confirmation, "Confirmation Message", DeleteMsgButtons);

            if (result == DialogResult.Yes)
            {
                // If IsVisible set true then it is approved and will be added to other views
                program.IsVisible = true;
                _context.SaveChanges();
            }
            return RedirectToAction("RequestedPrograms", "ArabicProgram");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RejectProgram(int id, string language)
        {

            ChangeLanguageFunction(language);

            var program = _context.Programs.Single(p => p.Id == id);

            DialogResult result = MessageBox.Show(StudentActivity.Resources.Language.Reject_program_confirmation, "Confirmation Message", DeleteMsgButtons);

            if (result == DialogResult.Yes)
            {
                // If IsVisible set true then it is approved and will be added to other views
                _context.Programs.Remove(program);
                _context.SaveChanges();
            }
            return RedirectToAction("RequestedPrograms", "ArabicProgram");
        }

    // END OF ADMIN ACTIONS
    
    // STUDENT ACTIONS
        // To show registered programs for student
        public ActionResult StuPrograms(string language)
        {

            ChangeLanguageFunction(language);

            var id = Session["Id"];
            var programs = _context.StudentPrograms.Include(p => p.Program).Include(c => c.Program.Club).Where(s => s.StudentId == id.ToString());
            return View(programs); 
        }

        

        // To let the student register for a program
        public ActionResult Registration(int programId, string language)
        {

            ChangeLanguageFunction(language);

            var studentId = Session["Id"].ToString();
            var StudentPrograms = new Student_Program(programId , studentId);
      
            return View("RegistrationForm",StudentPrograms);
        }

        // To save new student register for a program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Student_Program studentProgram, string language)
        {

            ChangeLanguageFunction(language);

            if (!ModelState.IsValid)
            {

                var StudentPrograms = new Student_Program();
                 
                return View("RegistrationForm", StudentPrograms);
            }

            _context.StudentPrograms.Add(studentProgram);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Student_already_registered_in_program, "Existence Error");
                return RedirectToAction("StuPrograms", "ArabicProgram");
            }
            

            return RedirectToAction("StuPrograms", "ArabicProgram");
        }

        public ActionResult DeleteStuPrg(String studentId, int programId, string language)
        {

            ChangeLanguageFunction(language);

            var StuPrg = _context.StudentPrograms.Where
                (s => s.StudentId == studentId)
                .Single(p => p.ProgramId == programId);

            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.StudentPrograms.Remove(StuPrg);
                _context.SaveChanges();
            }

            return RedirectToAction("StuPrograms", "ArabicProgram", new { id = StuPrg.StudentId });
        }

        // Show all available programs for the student
        public ActionResult ShowPrgStu(string language)
        {

            ChangeLanguageFunction(language);

            var programs = _context.Programs.Include(p => p.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);

            return View(programs);
        }

        // END OF STUDENT ACTION

        // CLUB COORDINATOR ACTIONS

        [Authorize(Roles = "CanManageClubs")]
        public ActionResult RequestProgram(string language)
        {

            ChangeLanguageFunction(language);

            var clubs = _context.Clubs.ToList();
            var viewModels = new ProgramViewModel()
            {
                program = new Program(),
                club = clubs
            };


            return View("RequestProgramForm", viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageClubs")]
        public ActionResult SaveReqPrg(Program program, string language)
        {

            ChangeLanguageFunction(language);

            if (!ModelState.IsValid)
            {
                var viewModels = new ProgramViewModel()
                {
                    program = new Program(),
                    club = _context.Clubs.ToList()
                };

                return View("RequestProgramForm", viewModels);
            }
            // To retrieve a program

            if (program.Id == 0)
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

            /* This exception will never occur because program id
               is auto generated by the database */

            _context.SaveChanges();
            return RedirectToAction("RequestedPrograms", "ArabicProgram");
        }
        // END OF CLUB COORDINATOR ACTIONS

        // SHARED ACTIONS

        // To show the details of a registered program in the student, admin and club cordinator view
        public ActionResult ProgramDetails(int id, string language)
        {

            ChangeLanguageFunction(language);

            var program = _context.Programs.Include(c => c.Club).SingleOrDefault(p => p.Id == id);
            return View(program);
        }

        // END OF SHARED ACTIONS

        public void ChangeLanguageFunction(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                HttpCookie cookie = new HttpCookie("Languages");
                cookie.Value = language;
                Response.Cookies.Add(cookie);
            }

        }

    }
}