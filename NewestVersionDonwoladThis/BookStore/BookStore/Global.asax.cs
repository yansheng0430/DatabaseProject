using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookStore.AuthenticationHelper; 

namespace BookStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static RSAHelper helper = new RSAHelper();
        //public static 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
        }
    }
}
