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
            Tuple<string, string> keySet = MvcApplication._RSAHelper.GenerateRSAKeys();
            MvcApplication._RSADataList.nowPublicKey = keySet.Item1;
            MvcApplication._RSADataList.nowPrivateKey = keySet.Item2;
            MvcApplication._RSADataList.PublicKeysList.Add(keySet.Item1);
            MvcApplication._RSADataList.PrivateKeysList.Add(keySet.Item2);
            AuthMember authMember = new AuthMember();
            authMember.PublicKey = keySet.Item1;
            return View(authMember);
        }

        [ValidateInput(false)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult MemberSignIn(AuthMember authMember)
        {
            //if (ModelState.IsValid)
            //{
            string encryptAccount = authMember.Account;
            string encryptPassword = authMember.Password;
            MvcApplication._RSADataList.nowEncryptAccount = encryptAccount;
            MvcApplication._RSADataList.nowEncryptPassword = encryptPassword;
            authMember.Account = MvcApplication._RSAHelper.Decrypt(MvcApplication._RSADataList.nowPrivateKey, encryptAccount);
            authMember.Password = MvcApplication._RSAHelper.Decrypt(MvcApplication._RSADataList.nowPrivateKey, encryptPassword);
            MvcApplication._RSADataList.AuthMemberString.Add(Tuple.Create(authMember.Account, authMember.Password));

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
            //}
            //else
            //{
              //  return View();
            //}
        }

        [Authorize]
        public ActionResult MemberSignOut()
        {
            _authManager.SignOut();
            return RedirectToAction("MemberSignIn", "Authentication");
        }

        [AllowAnonymous]
        public ActionResult DemoRSAPage()
        {
            return View(MvcApplication._RSADataList);
        }
    }
}