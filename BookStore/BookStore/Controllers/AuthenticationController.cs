using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class AuthenticationController : Controller
    {
        private AuthManager _authManager;

        public AuthenticationController()
        {
            _authManager = new AuthManager();
        }

        [AllowAnonymous]
        //Get: MemberSignIn
        public ActionResult MemberSignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult MemberSignIn(AuthMember authMember)
        {
            if (ModelState.IsValid)
            {
                if (authMember.Identity == "Customer" & _authManager.CustomerSignIn(authMember))
                {
                    return RedirectToAction("Index", "Customer");
                }
                else if (authMember.Identity == "Employee" & _authManager.EmployeeSignIn(authMember))
                {
                    return RedirectToAction("ProductList", "Employee");
                }
                else
                {
                    return View(authMember);
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult MemberSignOut()
        {
            _authManager.SignOut();
            return RedirectToAction("MemberSignIn", "Authentication");
        }
    }
}