using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        public ActionResult Index(string language)
        {
            ChangingLanguageFunction(language);

            var students = _context.Students.ToList();

            return View(students);
        }

        public ActionResult addStudent(string language)
        {
            ChangingLanguageFunction(language);

            var Student = new Student();
            return View("StudentForm");
        }

        [HttpPost]

        public ActionResult Save(Student student, string language)
        {
            ChangingLanguageFunction(language);

            if (!ModelState.IsValid)
            {
                var Student = new Student();

                return View("StudentForm");
            }
            _context.Students.Add(student);

            _context.SaveChanges();

            return RedirectToAction("Index", "Student");
        }

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