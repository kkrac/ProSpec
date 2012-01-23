using System.Web.Mvc;
using Sample.Domain.Services;
using Sample.UI.Web.Models.UserAccount;

namespace Sample.UI.Web.Controllers
{
    public class UserAccountController : SampleControllerBase
    {
        public UserAccountController(IUserAccountService service)
        {
            this.service = service;
        }

        private IUserAccountService service;

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserAccountNewViewModel user)
        {
            service.CreateAccount(user.ToModel(), user.Password, user.PasswordConfirmation, user.AcceptsTerms);

            return View("Temporary");
        }

        public ActionResult Confirm(string userId, string token)
        {
            bool isValid = service.IsTemporaryAccountValid(userId, token);

            if (isValid)
            {
                service.ActivateAccount(userId);

                return RedirectToAction("LogIn", "UserAccount");
            }
            else
            {
                return View("InvalidTemporary");
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(UserAccountLoginViewModel user)
        {
            service.ValidateLogin(user.UserName, user.Password);
            
            return RedirectToAction("Index", "Home");
        }
    }
}