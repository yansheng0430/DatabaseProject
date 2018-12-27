using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using BookStore.Authentication;

namespace BookStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static RSAViewModel _RSADataList = new Authentication.RSAViewModel();
        public static RSAHelper _RSAHelper = new RSAHelper();

        protected void Application_Start()
        {
            _RSADataList.PublicKeysList = new List<string>();
            _RSADataList.PrivateKeysList = new List<string>();
            _RSADataList.AuthMemberString = new List<Tuple<string, string>>();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                JObject jObject = JObject.Parse(ticket.UserData);
                string[] roles = { (string)jObject["Roles"] };
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
            }
        }
    }
}
