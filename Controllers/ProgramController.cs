﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using StudentActivity.Models;
using StudentActivity.ViewModel;

namespace StudentActivity.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program
        private ApplicationDbContext _context;

        // Attributes of delete pop warning message box 
        string DeleteMsgContent = StudentActivity.Resources.Language.Delete_Program_Confirmation;
        string DeleteMsgTitle = StudentActivity.Resources.Language.Delete_Program;
        MessageBoxButtons DeleteMsgButtons = MessageBoxButtons.YesNo;

        public ProgramController()
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
            ChangingLanguageFunction(language);

            var programs = _context.Programs.Include(p =>p.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/Index.cshtml",programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/Index.cshtml", programs);
            }
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RegisteredPrograms(string language)
        {
            ChangingLanguageFunction(language);

            var eligList = _context.Programs.Include(c => c.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RegisteredPrograms.cshtml", eligList);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RegisteredPrograms.cshtml", eligList);
            }
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EligibleList(int id, string language)
        {
            ChangingLanguageFunction(language);

            var students = _context.StudentPrograms.Include(s => s.Student).Where(p => p.ProgramId == id);


            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/EligibleList.cshtml", students);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/EligibleList.cshtml", students);
            }
        }



        // To add new student registered manually from the admin

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RegistrationFromAdmin(int id, string language)
        {
            ChangingLanguageFunction(language);

            var StudentPrograms = new Student_Program(id);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RegistrationFormAdmin.cshtml", StudentPrograms);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RegistrationFormAdmin.cshtml", StudentPrograms);
            }
        }

        // To save added to student_program table from the admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SaveFromAdmin(Student_Program studentProgram, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    var StudentPrograms = new Student_Program(studentProgram.ProgramId);

                    return View("~/Views/ArabicViews/ArabicProgram/RegistrationFormAdmin.cshtml", StudentPrograms);
                }
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    var StudentPrograms = new Student_Program(studentProgram.ProgramId);

                    return View("~/Views/EnglishViews/EnglishProgram/RegistrationFormAdmin.cshtml", StudentPrograms);
                }
            }

            _context.StudentPrograms.Add(studentProgram);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Added_student_already_registerd_in_program + StudentActivity.Resources.Language.Student_not_registerd_in_website, "Existence Error");
            }

            return RedirectToAction("EligibleList", "Program", new {id = studentProgram.ProgramId });
        }

        // To add a new program 
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult addProgram(string language)
        {
            ChangingLanguageFunction(language);

            var clubs = _context.Clubs.ToList();
            var viewModels = new ProgramViewModel()
            {
                program = new Program(),
                club = clubs          
            };

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/ProgramForm.cshtml", viewModels);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/ProgramForm.cshtml", viewModels);
            }
        }

        // To save the added program
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SavePrg(Program program, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    var viewModels = new ProgramViewModel()
                    {
                        program = new Program(),
                        club = _context.Clubs.ToList()
                    };

                    return View("~/Views/ArabicViews/ArabicProgram/ProgramForm.cshtml", viewModels);
                }
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    var viewModels = new ProgramViewModel()
                    {
                        program = new Program(),
                        club = _context.Clubs.ToList()
                    };

                    return View("~/Views/EnglishViews/EnglishProgram/ProgramForm.cshtml", viewModels);
                }
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
            return RedirectToAction("Index", "Program");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EditProgram(int id, string language)
        {
            ChangingLanguageFunction(language);

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

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/ProgramForm.cshtml", viewModel);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/ProgramForm.cshtml", viewModel);
            }
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult DeleteProgram(int id, string language)
        {
            ChangingLanguageFunction(language);

            var program = _context.Programs.Single(p => p.Id == id);

            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                // If IsDeleted set true then it is simply deleted in the view
                program.IsDeleted = true;
                _context.SaveChanges();
            } 

            return RedirectToAction("Index", "Program");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RetrievePrg(string language)
        {
            var deletedPrograms = _context.Programs.Where(m => m.IsDeleted == true).ToList();
            ChangingLanguageFunction(language);

            var delProgramViewModel = new ProgramViewModel
            {
                enumProgram = deletedPrograms,
                program = new Program()
            };
            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RetrievePrg.cshtml",delProgramViewModel);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RetrievePrg.cshtml", delProgramViewModel);
            }
        }
        
        // To save the program retrieve by admin
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SaveRetrievePrg(Program program, string language)
        {
            ChangingLanguageFunction(language);

            var deletedProgram = _context.Programs.SingleOrDefault(m => m.Id == program.Id);
            deletedProgram.IsDeleted = false;
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Program");
        }

        [Authorize(Roles = "CanManagePrograms" )]
        public ActionResult RequestedProgramsAdmin(string language)
        {
            ChangingLanguageFunction(language);

            var programs = _context.Programs.Include(p => p.Club).ToList().Where(p => p.IsVisible == false).Where(p => p.IsDeleted == false);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RequestedProgramsAdmin.cshtml", programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RequestedProgramsAdmin.cshtml", programs);
            }
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult ApproveProgram(int id, string language)
        {
            ChangingLanguageFunction(language);

            var program = _context.Programs.Single(p => p.Id == id);

            DialogResult result = MessageBox.Show(StudentActivity.Resources.Language.Approve_program_confirmation, "Confirmation Message", DeleteMsgButtons);

            if (result == DialogResult.Yes)
            {
                // If IsVisible set true then it is approved and will be added to other views
                program.IsVisible = true;
                program.PendingStatus = 1;  // 1 is for approved programs
                _context.SaveChanges();
            }
            return RedirectToAction("RequestedProgramsAdmin", "Program");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult RejectProgram(int id, string language)
        {
            ChangingLanguageFunction(language);

            var program = _context.Programs.Single(p => p.Id == id);

            DialogResult result = MessageBox.Show(StudentActivity.Resources.Language.Reject_program_confirmation, "Confirmation Message", DeleteMsgButtons);

            if (result == DialogResult.Yes)
            {
                // If IsVisible set true then it is approved and will be added to other views
                program.IsDeleted = true;
                program.PendingStatus = 2; // 2 stands for rejected Programs
                _context.SaveChanges();
            }
            return RedirectToAction("RequestedProgramsAdmin", "Program");
        }

        public ActionResult CompletedStuPrograms(string language)
        {
            ChangingLanguageFunction(language);

            var id = Session["Id"];
            var programs = _context.StudentPrograms.Include(s => s.Student).Include(p => p.Program).Include(c => c.Program.Club).Where(s => s.StudentId == id.ToString()).Where(s => s.IsEligible == true);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/CompletedStuPrograms.cshtml", programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/CompletedStuPrograms.cshtml", programs);
            }
        }

        public ActionResult WaterMarkExistingCertification(string studentName, string programTitle, string programTime, string programStartDate, string programEndDate)
        {
            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(Server.MapPath("/Images/Certificate.png"));
            string sValue = studentName;
            string tValue = programTitle;
            string lValue = programTime;
            string dValue = "From " + programStartDate + " To " + programEndDate;
            string file = "Certificate" + ".png";
            
            //using (Bitmap bitmap = new Bitmap(File.InputStream, false))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    if (studentName != null)
                    {
                        Brush brush = new SolidBrush(Color.Orange);

                        Font font = new Font("Arial", 60, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(sValue, font);

                        Point position = new Point(850, 400);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(sValue, font, brush, position, drawFormat);
                    }

                    if (programTitle != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(tValue, font);

                        Point position = new Point(850, 500);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(tValue, font, brush, position, drawFormat);
                    }

                    if (programTime != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(lValue, font);

                        Point position = new Point(850, 600);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(lValue, font, brush, position, drawFormat);
                    }

                    if (programStartDate != null  && programEndDate != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(dValue, font);

                        Point position = new Point(850, 700);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(dValue, font, brush, position, drawFormat);
                    }

                    using (MemoryStream mStream = new MemoryStream())
                    {
                        bitmap.Save(mStream, ImageFormat.Png);
                        mStream.Position = 0;
                        return File(mStream.ToArray(), "image/png", file);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult WaterMarkNewCertification(string student_name, string title, string lecturer_name, string date_time, HttpPostedFileBase postedFile)
        {
            string sValue = student_name;
            string tValue = title;
            string lValue = lecturer_name;
            string dValue = date_time;
            string file = Path.GetFileNameWithoutExtension(postedFile.FileName) + ".png";
            using (Bitmap bitmap = new Bitmap(postedFile.InputStream, false))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    if (postedFile != null)
                    {
                        Brush brush = new SolidBrush(Color.Orange);

                        Font font = new Font("Arial", 60, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(sValue, font);

                        Point position = new Point(850, 400);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(sValue, font, brush, position, drawFormat);
                    }

                    if (title != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(tValue, font);

                        Point position = new Point(850, 500);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(tValue, font, brush, position, drawFormat);
                    }

                    if (lecturer_name != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(lValue, font);

                        Point position = new Point(850, 600);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(lValue, font, brush, position, drawFormat);
                    }

                    if (lecturer_name != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(dValue, font);

                        Point position = new Point(850, 700);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(dValue, font, brush, position, drawFormat);
                    }

                    using (MemoryStream mStream = new MemoryStream())
                    {
                        bitmap.Save(mStream, ImageFormat.Png);
                        mStream.Position = 0;
                        return File(mStream.ToArray(), "image/png", file);
                    }
                }
            }
        }
        // END OF ADMIN ACTIONS

        // STUDENT ACTIONS
        // To show registered programs for student
        public ActionResult StuPrograms(string language)
        {
            ChangingLanguageFunction(language);

            var id = Session["Id"];
            var programs = _context.StudentPrograms.Include(p => p.Program).Include(c => c.Program.Club).Where(s => s.StudentId == id.ToString());

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/StuPrograms.cshtml", programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/StuPrograms.cshtml", programs);
            }
        }

        

        // To let the student register for a program
        public ActionResult Registration(int programId, string language)
        {
            ChangingLanguageFunction(language);

            var studentId = Session["Id"].ToString();
            var StudentPrograms = new Student_Program(programId , studentId);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RegistrationForm.cshtml", StudentPrograms);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RegistrationForm.cshtml", StudentPrograms);
            }
        }

        // To save new student register for a program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Student_Program studentProgram, string language)
        {
            ChangingLanguageFunction(language);

            /* if (!ModelState.IsValid)
             {

                 var StudentPrograms = new Student_Program();

                 return View("RegistrationForm", StudentPrograms);
             }*/

            _context.StudentPrograms.Add(studentProgram);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Student_already_registered_in_program, "Existence Error");
                return RedirectToAction("StuPrograms", "Program");
            }
            

            return RedirectToAction("StuPrograms", "Program");
        }

        public ActionResult DeleteStuPrg(String studentId, int programId, string language)
        {
            ChangingLanguageFunction(language);

            var StuPrg = _context.StudentPrograms.Where
                (s => s.StudentId == studentId)
                .Single(p => p.ProgramId == programId);

            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.StudentPrograms.Remove(StuPrg);
                _context.SaveChanges();
            }

            return RedirectToAction("StuPrograms", "Program", new { id = StuPrg.StudentId });
        }

        // Show all available programs for the student
        public ActionResult ShowPrgStu(string language)
        {
            ChangingLanguageFunction(language);

            var programs = _context.Programs.Include(p => p.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/ShowPrgStu.cshtml", programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/ShowPrgStu.cshtml", programs);
            }
        }

        [AllowAnonymous]
        public ActionResult VisitorAvailablePrograms(string language)
        {
            ChangingLanguageFunction(language);

            var programs = _context.Programs.Include(p => p.Club).ToList().Where(p => p.IsDeleted == false).Where(p => p.IsVisible == true);

            var clubs = _context.Clubs.ToList();

            List<ProgramClubsViewModel> list = new List<ProgramClubsViewModel>();

            list.Add(new ProgramClubsViewModel { Programs = programs });
            list.Add(new ProgramClubsViewModel { Clubs = clubs });

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/VisitorAvailablePrograms.cshtml", programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/VisitorAvailablePrograms.cshtml", programs);
            }
        }

        // END OF STUDENT ACTION

        // CLUB COORDINATOR ACTIONS

        [Authorize(Roles = "CanManageClubs")]
        public ActionResult RequestProgram(string language)
        {
            ChangingLanguageFunction(language);

            var clubs = _context.Clubs.ToList();
            var viewModels = new ProgramViewModel()
            {
                program = new Program(),
                club = clubs
            };

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RequestProgramForm.cshtml", viewModels);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RequestProgramForm.cshtml", viewModels);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageClubs")]
        public ActionResult SaveReqPrg(Program program, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    var viewModels = new ProgramViewModel()
                    {
                        program = new Program(),
                        club = _context.Clubs.ToList()
                    };

                    return View("~/Views/ArabicViews/ArabicProgram/RequestProgramForm.cshtml", viewModels);
                }
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    var viewModels = new ProgramViewModel()
                    {
                        program = new Program(),
                        club = _context.Clubs.ToList()
                    };

                    return View("~/Views/EnglishViews/EnglishProgram/RequestProgramForm.cshtml", viewModels);
                }
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
            return RedirectToAction("RequestedProgramsClubCor", "Program");
        }

        public ActionResult RemoveStatus(int id, String language)
        {
            ChangingLanguageFunction(language);
            var prg = _context.Programs.Single(P => P.Id == id);
            prg.ClubCorVisible = false;

            _context.SaveChanges();
            return RedirectToAction("RequestedProgramsClubCor", "Program");
        }

        [Authorize(Roles = "CanManageClubs")]
        public ActionResult RequestedProgramsClubCor(string language)
        {
            ChangingLanguageFunction(language);

            var programs = _context.Programs.Include(p => p.Club).ToList().Where(p => p.ClubCorVisible == true);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/RequestedProgramsClubCor.cshtml", programs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/RequestedProgramsClubCor.cshtml", programs);
            }
        }


        // END OF CLUB COORDINATOR ACTIONS

        // SHARED ACTIONS

        // To show the details of a registered program in the student, admin and club cordinator view
        public ActionResult ProgramDetails(int id, string language)
        {
            ChangingLanguageFunction(language);

            var program = _context.Programs.Include(c => c.Club).SingleOrDefault(p => p.Id == id);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/ProgramDetails.cshtml", program);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/ProgramDetails.cshtml", program);
            }
        }

        [AllowAnonymous]
        public ActionResult VisitorProgramDetails(int id, string language)
        {
            ChangingLanguageFunction(language);

            var program = _context.Programs.Include(c => c.Club).SingleOrDefault(p => p.Id == id);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicProgram/VisitorProgramDetails.cshtml", program);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishProgram/VisitorProgramDetails.cshtml", program);
            }
        }

        // END OF SHARED ACTIONS

        public void ChangingLanguageFunction(string language)
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