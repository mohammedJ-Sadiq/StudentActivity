using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace StudentActivity.Controllers
{
    
    public class ArabicHomeController : Controller
    {
        public ActionResult Home(string language)
        {

            if (User.IsInRole("CanManagePrograms"))
            {
                ChangeLanguageFunction(language);
                return View("AdminHome");
            }
                
            else if (User.IsInRole("CanManageClubs"))
            {
                ChangeLanguageFunction(language);
                return View("ClubCorHome");
            }
            ChangeLanguageFunction(language);
            return View("StudentHome");
        }

        [AllowAnonymous]
        public ActionResult Index(string language)
        {

            ChangeLanguageFunction(language);

            return View();
        }

        public ActionResult StudentHome(string language)
        {

            ChangeLanguageFunction(language);

            return View();
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AdminHome(string language)
        {

            ChangeLanguageFunction(language);

            return View();
        }

        [Authorize(Roles ="CanManageClubs")]
        public ActionResult ClubCorHome(string language)
        {

            ChangeLanguageFunction(language);

            return View();
        }


        public ActionResult About(string language)
        {

            ChangeLanguageFunction(language);

            ViewBag.Message = StudentActivity.Resources.Language.About_page;

            return View();
        }

        public ActionResult Contact(string language)
        {

            ChangeLanguageFunction(language);

            ViewBag.Message = StudentActivity.Resources.Language.Contact;

            return View();
        }

        public void ChangeLanguageFunction(string language)
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