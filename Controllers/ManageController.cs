using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentActivity.Models;

namespace StudentActivity.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message, string language)
        {
            ChangingLanguageFunction(language);

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? StudentActivity.Resources.Language.Password_has_been_changed
                : message == ManageMessageId.SetPasswordSuccess ? StudentActivity.Resources.Language.Password_has_been_set
                : message == ManageMessageId.SetTwoFactorSuccess ? StudentActivity.Resources.Language.Two_factor_authentication_provider_has_been_set
                : message == ManageMessageId.Error ? StudentActivity.Resources.Language.Error_occured
                : message == ManageMessageId.AddPhoneSuccess ? StudentActivity.Resources.Language.Phone_number_added
                : message == ManageMessageId.RemovePhoneSuccess ? StudentActivity.Resources.Language.Phone_number_removed
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/Index.cshtml", model);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/Index.cshtml", model);
            }
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey, string language)
        {
            ChangingLanguageFunction(language);

            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/AddPhoneNumber.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/AddPhoneNumber.cshtml");
            }
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/ArabicViews/ArabicManage/AddPhoneNumber.cshtml", model);
                }
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/EnglishViews/EnglishManage/AddPhoneNumber.cshtml", model);
                }
            }

            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication(string language)
        {
            ChangingLanguageFunction(language);

            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication(string language)
        {
            ChangingLanguageFunction(language);

            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber, string language)
        {
            ChangingLanguageFunction(language);

            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number

            if (language.Equals("ar"))
            {
                return phoneNumber == null ? View("Error") : View("~/Views/ArabicViews/ArabicManage/VerifyPhoneNumber.cshtml", new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
            }

            else
            {
                return phoneNumber == null ? View("Error") : View("~/Views/EnglishViews/EnglishManage/VerifyPhoneNumber.cshtml", new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
            }
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/ArabicViews/ArabicManage/VerifyPhoneNumber.cshtml", model);
                }
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/EnglishViews/EnglishManage/VerifyPhoneNumber.cshtml", model);
                }
                
            }

            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", StudentActivity.Resources.Language.Verify_phone_failed);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/VerifyPhoneNumber.cshtml", model);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/VerifyPhoneNumber.cshtml", model);
            }
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber(string language)
        {
            ChangingLanguageFunction(language);

            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/ChangePassword.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/ChangePassword.cshtml");
            }
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model, string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/ArabicViews/ArabicManage/ChangePassword.cshtml", model);
                }
                
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/EnglishViews/EnglishManage/ChangePassword.cshtml", model);
                }
                
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/ChangePassword.cshtml", model);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/ChangePassword.cshtml", model);
            }
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword(string language)
        {
            ChangingLanguageFunction(language);

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/SetPassword.cshtml");
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/SetPassword.cshtml");
            }
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model, string language)
        {
            ChangingLanguageFunction(language);

            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/SetPassword.cshtml", model);
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/SetPassword.cshtml", model);
            }
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message, string language)
        {
            ChangingLanguageFunction(language);

            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? StudentActivity.Resources.Language.External_login_removed
                : message == ManageMessageId.Error ? StudentActivity.Resources.Language.Error_occured
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;

            if (language.Equals("ar"))
            {
                return View("~/Views/ArabicViews/ArabicManage/ManageLogins.cshtml", new ManageLoginsViewModel
                {
                    CurrentLogins = userLogins,
                    OtherLogins = otherLogins
                });
            }

            else
            {
                return View("~/Views/EnglishViews/EnglishManage/ManageLogins.cshtml", new ManageLoginsViewModel
                {
                    CurrentLogins = userLogins,
                    OtherLogins = otherLogins
                });
            }
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider, string language)
        {
            ChangingLanguageFunction(language);

            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback(string language)
        {
            ChangingLanguageFunction(language);

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
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
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}