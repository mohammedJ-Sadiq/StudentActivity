﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;
using System.Data.Entity;
using StudentActivity.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace StudentActivity.Controllers
{
    public class ClubController : Controller
    {
        // GET: Club
        string DeleteMsgContent = StudentActivity.Resources.Language.DeleteConfirmation;
        string DeleteMsgTitle = StudentActivity.Resources.Language.Delete_field;
        MessageBoxButtons DeleteMsgButtons = MessageBoxButtons.YesNo;

        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ClubController()
        {
            _context = new ApplicationDbContext();
        }

        public ClubController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // ADMIN ACTIONS

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult Index(string language)
        {
            ChangingLanguageFunction(language);

            var clubs = _context.Clubs.Include(c => c.Student).ToList();

            //-----------------------------------------------------------------------------------------------------------------------------------
            // Checks if the Specified TempData has Data or not, then Create a ViewBag with a Variable that specifies The Name of the Alert in Integer Value
            // Then Send it to The View

            if (TempData.ContainsKey("AddingClubByAdminExistingErrorMessage"))
            {
                ViewBag.AddingClubByAdminExistingErrorMessage = int.Parse(TempData["AddingClubByAdminExistingErrorMessage"].ToString());
            }

            if (TempData.ContainsKey("EditingClubByAdminSuccessMessage"))
            {
                ViewBag.EditingClubByAdminSuccessMessage = int.Parse(TempData["EditingClubByAdminSuccessMessage"].ToString());
            }

            if (TempData.ContainsKey("AddingClubByAdminSuccessMessage"))
            {
                ViewBag.AddingClubByAdminSuccessMessage = int.Parse(TempData["AddingClubByAdminSuccessMessage"].ToString());
            }

            if (TempData.ContainsKey("DeletingClubByAdminSuccessMessage"))
            {
                ViewBag.DeletingClubByAdminSuccessMessage = int.Parse(TempData["DeletingClubByAdminSuccessMessage"].ToString());
            }

            //-----------------------------------------------------------------------------------------------------------------------------------

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicClub/Index.cshtml", clubs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishClub/Index.cshtml", clubs);
            }
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AddClub(string language)
        {
            ChangingLanguageFunction(language);

            var Club = new Club();

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicClub/ClubForm.cshtml", Club);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishClub/ClubForm.cshtml", Club);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public async Task<ActionResult> SaveClub(Club club, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    var Club = new Club();

                    return View("~/Views/ArabicViews/ArabicClub/ClubForm.cshtml");
                }
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    var Club = new Club();

                    return View("~/Views/EnglishViews/EnglishClub/ClubForm.cshtml");
                }
            }

            if (club.Id == 0)
            {
                _context.Clubs.Add(club);
                var student = _context.Users.SingleOrDefault(c => c.UserName == club.StudentId);
                var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                await roleManager.CreateAsync(new IdentityRole("CanManageClubs"));
                await UserManager.AddToRoleAsync(student.Id, "CanManageClubs");
                //SignInManager.SignIn(student, isPersistent: false, rememberBrowser: false);

                // Sends AddingClubByAdminSuccessMessage to the another action using the TempData
                TempData["AddingClubByAdminSuccessMessage"] = 1;

            }
            else
            {
                var ClubInDb = _context.Clubs.Single(c => c.Id == club.Id);
                ClubInDb.Name = club.Name;
                ClubInDb.StudentId = club.StudentId;
                ClubInDb.ClubVisionEng = club.ClubVisionEng;
                ClubInDb.ClubVisionAr = club.ClubVisionAr;

                // Sends EditingClubByAdminSuccessMessage to the another action using the TempData
                TempData["EditingClubByAdminSuccessMessage"] = 1;

            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //MessageBox.Show(StudentActivity.Resources.Language.Club_already_Exist);

                // Sends AddingClubByAdminExistingErrorMessage to the another action using the TempData
                TempData["AddingClubByAdminExistingErrorMessage"] = 1;
            }

            return RedirectToAction("Index", "Club");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EditClub(int id, string language)
        {
            ChangingLanguageFunction(language);

            var club = _context.Clubs.Include(s => s.Student).SingleOrDefault(c => c.Id == id);

            ViewBag.EditedClubName = club.Name;

            if (club == null)
            {
                return HttpNotFound();
            }

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicClub/ClubForm.cshtml", club);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishClub/ClubForm.cshtml", club);
            }

        }

        // To let the admin delete a club permanently - not only from view
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult DeleteClub(int id, string language)
        {
            ChangingLanguageFunction(language);

            var club = _context.Clubs.SingleOrDefault(c => c.Id == id);
            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }

            // Sends DeletingClubByAdminSuccessMessage to the another action using the TempData
            TempData["DeletingClubByAdminSuccessMessage"] = 1;

            return RedirectToAction("Index", "Club");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult ShowStuOfClub(int id, string language)
        {
            ChangingLanguageFunction(language);

            var students = _context.StudentClubs.Include(s => s.Student).
                Where(c => c.ClubId == id);
            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicClub/ShowStuOfClub.cshtml", students);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishClub/ShowStuOfClub.cshtml", students);
            }
        }

        // END OF ADMIN ACTIONS

        // STUDENT ACTIONS

        public ActionResult AddStuClubs(string language)
        {
            ChangingLanguageFunction(language);

            var clubs = _context.Clubs.ToList();
            //var studentId = Session["Id"].ToString();
            var viewModels = new StudentClubViewModel()
            {
                StudentClub = new Student_Club(Session["Id"].ToString()),
                Clubs = clubs
            };


            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicClub/StuClubsForm.cshtml", viewModels);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishClub/StuClubsForm.cshtml", viewModels);
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult SaveStuClub(Student_Club studentClub, string language)
        {
            ChangingLanguageFunction(language);

            if (!ModelState.IsValid)
            {
                var clubs = _context.Clubs.ToList();
                var viewModels = new StudentClubViewModel()
                {
                    StudentClub = new Student_Club(Session["Id"].ToString()),
                    Clubs = clubs
                };

            }
            _context.StudentClubs.Add(studentClub);

            try
            {
                _context.SaveChanges();

                // Sends StudentClubAddedMessage to the another action using the TempData
                TempData["StudentClubAddedMessage"] = 1;
            }
            catch (DbUpdateException)
            {
                //MessageBox.Show(StudentActivity.Resources.Language.Already_a_member_of_the_club, "Existence Error");

                // Sends StudentClubRegisteredErrorMessage to the another action using the TempData
                TempData["StudentClubRegisteredErrorMessage"] = 1;

                return RedirectToAction("ShowClubs", "Club");
            }

            return RedirectToAction("ShowClubs", "Club");
        }

        // To show all clubs registered by a specific student
        public ActionResult ShowClubs(string language)
        {
            ChangingLanguageFunction(language);

            //-----------------------------------------------------------------------------------------------------------------------------------
            // Checks if the Specified TempData has Data or not, then Create a ViewBag with a Variable that specifies The Name of the Alert in Integer Value
            // Then Send it to The View

            if (TempData.ContainsKey("StudentClubRegisteredErrorMessage"))
            {
                ViewBag.StudentClubRegisteredErrorMessage = int.Parse(TempData["StudentClubRegisteredErrorMessage"].ToString());
            }

            if (TempData.ContainsKey("StudentClubLeaveMessage"))
            {
                ViewBag.StudentClubLeaveMessage = int.Parse(TempData["StudentClubLeaveMessage"].ToString());
            }

            if (TempData.ContainsKey("StudentClubAddedMessage"))
            {
                ViewBag.StudentClubAddedMessage = int.Parse(TempData["StudentClubAddedMessage"].ToString());
            }

            //-----------------------------------------------------------------------------------------------------------------------------------

            var id = Session["Id"].ToString();
            var clubs = _context.StudentClubs.Include(c => c.Club).Where(s => s.StudentId == id);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicClub/ShowClubs.cshtml", clubs);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishClub/ShowClubs.cshtml", clubs);
            }

        }

        public ActionResult DeleteStuClub(String studentId, int clubId, string language)
        {
            ChangingLanguageFunction(language);

            var studentClub = _context.StudentClubs.Where
                (s => s.StudentId == studentId)
                .Single(c => c.ClubId == clubId);


            _context.StudentClubs.Remove(studentClub);
            _context.SaveChanges();

            // Sends StudentClubLeaveMessage to the another action using the TempData
            TempData["StudentClubLeaveMessage"] = 1;


            return RedirectToAction("ShowClubs", "Club");
        }

        // END OF STUDENT ACTIONS
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