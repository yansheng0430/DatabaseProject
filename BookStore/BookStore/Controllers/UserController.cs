using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.Web.Security;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        private UserManager _userManager;
        public UserController()
        {
            _userManager = new UserManager();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(AuthenticationUser aUser)
        {
            if (!(aUser.Account == "aaa" && aUser.Passward == "123"))
            {
                return RedirectToAction("SignIn");

            }
            else
            {
                Models.User user = new User
                {
                    CustomerID = "C123456789",
                    FirstName = "Boy",
                    LastName = "Chen",
                    Sex = "0",
                    Cellphone = "0915511445",
                    Email = "aaa@gamil.com",
                    Account = "aaa",
                    Passward = "123"
                };
                _userManager.SignIn(user);
                //Response.Redirect(FormsAuthentication.DefaultUrl);
                return RedirectToAction("EmployeeHome", "Employee");
            }
            
        }

        
        public void SignOut()
        {
            _userManager.SignOut();
            Response.Redirect(FormsAuthentication.DefaultUrl);
        }

        [HttpPost]
        public string IsAuthenticated()
        {
            User user = _userManager.GetUser();
            if (user == null)
            {
                return "SignIn Successfully";
            }
            return "SignIn Unsuccessfully";
        }
    }
}