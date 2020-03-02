using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;

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

       /* public ActionResult Index()
        {
            return View();
        }*/

    }
}