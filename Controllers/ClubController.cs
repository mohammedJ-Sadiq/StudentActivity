using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;
using System.Data.Entity;
using StudentActivity.ViewModel;

namespace StudentActivity.Controllers
{
    public class ClubController : Controller
    {
        // GET: Club
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
        public ActionResult Index()
        {
            var clubs = _context.Clubs.Include(c => c.Student).ToList();
            return View(clubs);
        }

        public ActionResult AddClub()
        {
            var Club = new Club();

            return View("ClubForm",Club);
        }

        [HttpPost]

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

            _context.SaveChanges();

            return RedirectToAction("Index", "Club");
        }

        public ActionResult EditClub(int id)
        {
            var club = _context.Clubs.Include(s =>s.Student).SingleOrDefault(c => c.Id == id);
            if (club == null)
            {
                return HttpNotFound();
            }

            return View("ClubForm", club);
        }

    // END OF ADMIN ACTIONS
        
    // STUDENT ACTIONS

        public ActionResult AddStuClubs()
        {
            var clubs = _context.Clubs.ToList();
            var viewModels = new StudentClubViewModel()
            {
                StudentClub = new Student_Club(),
                Clubs = clubs
            };

            return View("StuClubsForm", viewModels);
        }

        public ActionResult SaveStuClub(Student_Club studentClub)
        {
            if (!ModelState.IsValid)
            {
                var clubs = _context.Clubs.ToList();
                var viewModels = new StudentClubViewModel()
                {
                    StudentClub = new Student_Club(),
                    Clubs = clubs
                };

            }
            _context.StudentClubs.Add(studentClub);

            _context.SaveChanges();

            return RedirectToAction("ShowClubs", "Club");
        }

        // To show all clubs registered by a specific student
        // NOT COMPLETED, must pass student id when the session is created
        public ActionResult ShowClubs(string id)
        {
            var clubs = _context.StudentClubs.Include(c => c.Club).Where(s => s.StudentId == id);
            return View("ShowClubs", clubs);
        }

    // END OF STUDENT ACTIONS
    }
}