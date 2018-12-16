using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookManaged
    {
        public string EmployeeID { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public string ActionState { get; set; }    
    }
}