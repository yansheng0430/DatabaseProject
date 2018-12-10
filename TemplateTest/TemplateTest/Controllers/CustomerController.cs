using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TemplateTest.Models;

namespace TemplateTest.Controllers
{
    public class CustomerController : Controller
    {
        private AuthManager _authManager;

        public CustomerController()
        {
            _authManager = new AuthManager();
        }

        [AllowAnonymous]
        // GET: Index
        public ActionResult Index()
        { 
            return View();
        }

        [Authorize]
        // Get: Shop
        public ActionResult Shop()
        {
            return View();
        }

        [AllowAnonymous]
        //Get: About
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        //Get: Recommand
        public ActionResult Recommand()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MemberSignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult MemberSignIn(AuthMember authCustomer)
        {
            if (ModelState.IsValid)
            {
                if (_authManager.SignIn(authCustomer))
                    return RedirectToAction("Index", "Customer");
                else
                    return View(authCustomer);
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
            return RedirectToAction("Index", "Customer");
        }
        /*
        [Authorize]
        public ActionResult MemberInformation()
        {
            _authManager.SignOut();
            return View()
        }
        */
        [AllowAnonymous]
        public ActionResult AccountCreate()
        {
            return View();
        }
    }
}