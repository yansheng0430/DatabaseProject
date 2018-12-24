using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BookStore.DAO;
using Newtonsoft.Json;

namespace BookStore.Models
{
    public class AuthManager
    {
        public bool CustomerSignIn(AuthMember authMember)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            Customer authCustomer = customerDAO.GetCustomerByAuthentication(authMember);

            if (authCustomer != null)
            {
                authCustomer.Roles = "User";
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                authCustomer.CustomerID,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                false,
                JsonConvert.SerializeObject(authCustomer),
                FormsAuthentication.FormsCookiePath
                );

                string encTicketString = FormsAuthentication.Encrypt(ticket);
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicketString));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EmployeeSignIn(AuthMember authMember)
        {
            EmployeeDAO employeeDAO = new EmployeeDAO();
            Employee authEmployee = employeeDAO.GetEmployeeByAuthentication(authMember);
            if (authEmployee != null)
            {
                authEmployee.Roles = "Admin";
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                authEmployee.EmployeeID,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                false,
                JsonConvert.SerializeObject(authEmployee),
                FormsAuthentication.FormsCookiePath
                );

                string encTicketString = FormsAuthentication.Encrypt(ticket);
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicketString));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}