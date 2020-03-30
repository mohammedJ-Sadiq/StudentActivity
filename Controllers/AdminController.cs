﻿using StudentActivity.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using StudentActivity.ViewModel;

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

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult Index(string language)
        {
            ChangingLanguageFunction(language);

            return View();
        }
        
        // To Save the value of eligibility check box in eligilble list
        [HttpPost]
        public ActionResult SaveEligibility(Student_Program studentProgram, string language)
        {
            ChangingLanguageFunction(language);

            var StuPrgInDb = _context.StudentPrograms.ToList().Where(p => p.ProgramId == studentProgram.ProgramId);

            var id = Request.Form[""];
            
            foreach (var item in StuPrgInDb)
            {
                id = Request.Form[item.StudentId.ToString()];
                if ("true" == id)
                    item.IsEligible = true;
                else
                    item.IsEligible = false;
                    
            }
            _context.SaveChanges();

            return RedirectToAction("EligibleList", "Program", new { id = studentProgram.ProgramId });
           
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