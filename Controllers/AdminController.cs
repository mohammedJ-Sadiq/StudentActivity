using StudentActivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentActivity.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }
        /*
        public ActionResult SaveEligibility()
        {

        }
        */
        
    }
}