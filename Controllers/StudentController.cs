using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentActivity.Models;

namespace StudentActivity.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        private ApplicationDbContext _context;

        public StudentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var students = _context.Students.ToList();

            return View(students);
        }
    }
}