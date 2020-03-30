using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace StudentActivity.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Home(string language)
        {
            ChangingLanguageFunction(language);

            if (User.IsInRole("CanManagePrograms"))
                return View("AdminHome");
            else if (User.IsInRole("CanManageClubs"))
                return View("ClubCorHome");
            return View("StudentHome");
        }

        [AllowAnonymous]
        public ActionResult Index(string language)
        {
            ChangingLanguageFunction(language);

            return View();
        }

        public ActionResult StudentHome(string language)
        {
            ChangingLanguageFunction(language);

            return View();
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AdminHome(string language)
        {
            ChangingLanguageFunction(language);

            return View();
        }

        [Authorize(Roles ="CanManageClubs")]
        public ActionResult ClubCorHome(string language)
        {
            ChangingLanguageFunction(language);

            return View();
        }


        public ActionResult About(string language)
        {
            ChangingLanguageFunction(language);

            ViewBag.Message = StudentActivity.Resources.Language.About_page;

            return View();
        }

        public ActionResult Contact(string language)
        {
            ChangingLanguageFunction(language);

            ViewBag.Message = StudentActivity.Resources.Language.Contact_page;

            return View();
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