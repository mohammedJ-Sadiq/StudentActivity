﻿using System;
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

            return View("ClubForm");
        }

        [HttpPost]

        public ActionResult Save(Club club)
        {
            if (!ModelState.IsValid)
            {
                var Club = new Club();

                return View("ClubForm");

            }
            _context.Clubs.Add(club);

            _context.SaveChanges();

            return RedirectToAction("Index", "Club");
        }

        // END OF ADMIN ACTIONS

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

    }
}