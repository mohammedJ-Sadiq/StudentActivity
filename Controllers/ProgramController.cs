using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;

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
            var programs = _context.Programs.Include(p => p.ClubName).ToList();

            return View(programs);
        }
    }
}