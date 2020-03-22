using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentActivity.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            if (User.IsInRole("CanManagePrograms"))
                return View("AdminHome");
            else if (User.IsInRole("CanManageClubs"))
                return View("ClubCorHome");
            return View("StudentHome");
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentHome()
        {
            return View();
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AdminHome()
        {
            return View();
        }

        [Authorize(Roles ="CanManageClubs")]
        public ActionResult ClubCorHome()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}