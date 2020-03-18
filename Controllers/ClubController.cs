using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;
using System.Data.Entity;
using StudentActivity.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;

namespace StudentActivity.Controllers
{
    public class ClubController : Controller
    {
        // GET: Club
        string DeleteMsgContent = "Are you sure you want to delete this field ?";
        string DeleteMsgTitle = "Delete field";
        MessageBoxButtons DeleteMsgButtons = MessageBoxButtons.YesNo;

        private ApplicationDbContext _context;

        public ClubController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // ADMIN ACTIONS

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult Index()
        {
            var clubs = _context.Clubs.Include(c => c.Student).ToList();
            return View(clubs);
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AddClub()
        {
            var Club = new Club();

            return View("ClubForm",Club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult SaveClub(Club club)
        {
            if (!ModelState.IsValid)
            {
                var Club = new Club();

                return View("ClubForm");

            }

            if(club.Id == 0)
            {
                _context.Clubs.Add(club);
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
                MessageBox.Show("The Club you are " +
                    "trying to add is already Exist");
            }

            return RedirectToAction("Index", "Club");
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult EditClub(int id)
        {
            var club = _context.Clubs.Include(s =>s.Student).SingleOrDefault(c => c.Id == id);
            if (club == null)
            {
                return HttpNotFound();
            }

            return View("ClubForm", club);
        }

        // To let the admin delete a club permanently - not only from view
        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult DeleteClub(int id)
        {
            var club = _context.Clubs.SingleOrDefault(c => c.Id == id);
            DialogResult result = MessageBox.Show(DeleteMsgContent, DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction("Index","Club");
        }

    // END OF ADMIN ACTIONS
        
    // STUDENT ACTIONS

        public ActionResult AddStuClubs()
        {
            var clubs = _context.Clubs.ToList();
            //var studentId = Session["Id"].ToString();
            var viewModels = new StudentClubViewModel()
            {
                StudentClub = new Student_Club(Session["Id"].ToString()),
                Clubs = clubs
            };

            return View("StuClubsForm", viewModels);
        }

        [ValidateAntiForgeryToken]
        public ActionResult SaveStuClub(Student_Club studentClub)
        {
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
                MessageBox.Show("The club you are " +
                    "trying to join,\nyou are already part of it", "Existence Error");
            }

            return RedirectToAction("ShowClubs", "Club");
        }

        // To show all clubs registered by a specific student
        // NOT COMPLETED, must pass student id when the session is created
        public ActionResult ShowClubs()
        {
            var id = Session["Id"].ToString();
            var clubs = _context.StudentClubs.Include(c => c.Club).Where(s => s.StudentId == id);
            return View("ShowClubs", clubs);
        }

        public ActionResult DeleteStuClub(String studentId, int clubId)
        {
            var studentClub = _context.StudentClubs.Where
                (s => s.StudentId == studentId)
                .Single(c => c.ClubId == clubId);

            DialogResult result = MessageBox.Show("Are you sure " +
                " you want to leave this club", DeleteMsgTitle, DeleteMsgButtons);
            if (result == DialogResult.Yes)
            {
                _context.StudentClubs.Remove(studentClub);
                _context.SaveChanges();
            }

            return RedirectToAction("ShowClubs", "Club");
        }

    // END OF STUDENT ACTIONS
    }
}