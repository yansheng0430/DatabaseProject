using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class User
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Passward { get; set; }
    }
}