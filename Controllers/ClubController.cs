using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;
using System.Data.Entity;

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

        public ActionResult Index()
        {
            var clubs = _context.Clubs.Include(c => c.CoordinatorName).ToList();
            return View(clubs);
        }

    }
}