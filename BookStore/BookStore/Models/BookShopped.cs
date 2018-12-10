using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookShopped
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; } 
        public int Subtotal
        {
            get
            {
                return this.UnitPrice * this.Amount;
            }
            set { }
        }
        public string Cover { get; set; }
    }
}