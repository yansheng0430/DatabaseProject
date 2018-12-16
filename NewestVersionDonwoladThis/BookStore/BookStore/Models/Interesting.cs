using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BookStore.Models
{
    public class Interesting
    {
        public string CustomerID { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Cover { get; set; }
        public DateTime AddDate { get; set; }
    }
}