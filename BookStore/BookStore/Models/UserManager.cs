using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace BookStore.Models
{
    public class UserManager
    {
        public void SignIn(User user)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
            (
                1,
                user.FirstName,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                false,
                JsonConvert.SerializeObject(user),
                FormsAuthentication.FormsCookiePath
            );

            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Response.Cookies.Add
            (
                new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            );
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public User GetUser()
        {
            var user = HttpContext.Current.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var identity = (FormsIdentity)user.Identity;
                var ticket = identity.Ticket;
                return JsonConvert.DeserializeObject<User>(ticket.UserData);
            }
            return null;
        }
    }
}