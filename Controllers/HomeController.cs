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

            if (language.Equals("ar"))
            {
                if (User.IsInRole("CanManagePrograms"))
                    return View("~/Views/ArabicViews/ArabicHome/AdminHome.cshtml");
                else if (User.IsInRole("CanManageClubs"))
                    return View("~/Views/ArabicViews/ArabicHome/ClubCorHome.cshtml");
                return View("~/Views/ArabicViews/ArabicHome/StudentHome.cshtml");
            }

            else
            {
                if (User.IsInRole("CanManagePrograms"))
                    return View("~/Views/EnglishViews/EnglishHome/AdminHome.cshtml");
                else if (User.IsInRole("CanManageClubs"))
                    return View("~/Views/EnglishViews/EnglishHome/ClubCorHome.cshtml");
                return View("~/Views/EnglishViews/EnglishHome/StudentHome.cshtml");
            }
            
        }

        [AllowAnonymous]
        public ActionResult Index(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicHome/Index.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishHome/Index.cshtml");
            }
        }

        public ActionResult StudentHome(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicHome/StudentHome.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishHome/StudentHome.cshtml");
            }
        }

        [Authorize(Roles = "CanManagePrograms")]
        public ActionResult AdminHome(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicHome/AdminHome.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishHome/AdminHome.cshtml");
            }
        }

        [Authorize(Roles ="CanManageClubs")]
        public ActionResult ClubCorHome(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicHome/ClubCorHome.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishHome/ClubCorHome.cshtml");
            }
        }


        public ActionResult About(string language)
        {
            ChangingLanguageFunction(language);

            ViewBag.Message = StudentActivity.Resources.Language.About_page;

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicHome/About.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishHome/About.cshtml");
            }
        }

        public ActionResult Contact(string language)
        {
            ChangingLanguageFunction(language);

            ViewBag.Message = StudentActivity.Resources.Language.Contact_page;

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicHome/Contact.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishHome/Contact.cshtml");
            }
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