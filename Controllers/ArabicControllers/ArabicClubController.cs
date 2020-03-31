using System;
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
    public class ArabicClubController : Controller
    {
        // GET: Club
        string DeleteMsgContent = StudentActivity.Resources.Language.DeleteConfirmation;
        string DeleteMsgTitle = StudentActivity.Resources.Language.Delete_field;
        MessageBoxButtons DeleteMsgButtons = MessageBoxButtons.YesNo;

        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ArabicClubController()
        {
            _context = new ApplicationDbContext();
        }

        public ArabicClubController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

            ChangeLanguageFunction(language);

            var clubs = _context.Clubs.Include(c => c.Student).ToList();
            return View(clubs);
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AddClub(string language)
        {

            ChangeLanguageFunction(language);

            var Club = new Club();

            return View("ClubForm",Club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public async Task<ActionResult> SaveClub(Club club, string language)
        {

            ChangeLanguageFunction(language);

            if (!ModelState.IsValid)
            {
                var Club = new Club();

                return View("ClubForm");

            }

            if(club.Id == 0)
            {
                _context.Clubs.Add(club);
                var student = _context.Users.SingleOrDefault(c => c.UserName == club.StudentId);
                var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                await roleManager.CreateAsync(new IdentityRole("CanManageClubs"));
                await UserManager.AddToRoleAsync(student.Id, "CanManageClubs");
                //SignInManager.SignIn(student, isPersistent: false, rememberBrowser: false);

            }
            else
            {
                var ClubInDb = _context.Clubs.Single(c => c.Id == club.Id);
                ClubInDb.Name = club.Name;
                ClubInDb.StudentId = club.StudentId;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Club_already_Exist);
            }

            return RedirectToAction("Index", "ArabicClub");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EditClub(int id, string language)
        {

            ChangeLanguageFunction(language);

            var club = _context.Clubs.Include(s =>s.Student).SingleOrDefault(c => c.Id == id);
            if (club == null)
            {
                return HttpNotFound();
            }

            return View("ClubForm", club);
        }

        // To let the admin delete a club permanently - not only from view
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult DeleteClub(int id, string language)
        {

            ChangeLanguageFunction(language);

            var club = _context.Clubs.SingleOrDefault(c => c.Id == id);
            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction("Index","ArabicClub");
        }

    // END OF ADMIN ACTIONS
        
    // STUDENT ACTIONS

        public ActionResult AddStuClubs(string language)
        {

            ChangeLanguageFunction(language);

            var clubs = _context.Clubs.ToList();
            var studentId = Session["Id"].ToString();
            var viewModels = new StudentClubViewModel()
            {
                StudentClub = new Student_Club(Session["Id"].ToString()),
                Clubs = clubs
            };

            return View("StuClubsForm", viewModels);
        }

        [ValidateAntiForgeryToken]
        public ActionResult SaveStuClub(Student_Club studentClub, string language)
        {

            ChangeLanguageFunction(language);

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
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(StudentActivity.Resources.Language.Already_a_member_of_the_club);
            }

            return RedirectToAction("ShowClubs", "ArabicClub");
        }

        // To show all clubs registered by a specific student
        public ActionResult ShowClubs(string language)
        {

            ChangeLanguageFunction(language);

            var id = Session["Id"].ToString();
            var clubs = _context.StudentClubs.Include(c => c.Club).Where(s => s.StudentId == id);
            return View("ShowClubs", clubs);
        }

        public ActionResult DeleteStuClub(String studentId, int clubId, string language)
        {

            ChangeLanguageFunction(language);

            var studentClub = _context.StudentClubs.Where
                (s => s.StudentId == studentId)
                .Single(c => c.ClubId == clubId);

            DialogResult result = MessageBox.Show(StudentActivity.Resources.Language.Leave_Club, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.StudentClubs.Remove(studentClub);
                _context.SaveChanges();
            }

            return RedirectToAction("ShowClubs", "ArabicClub");
        }

        // END OF STUDENT ACTIONS

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