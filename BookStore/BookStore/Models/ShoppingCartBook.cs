using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ShoppingCartBook
    {
        public string CustomerID { get; set; }
        public string ISBN { get; set; }
        public int Amount { get; set; }
    }
}