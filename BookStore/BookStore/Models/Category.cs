using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Category
    {
        public string CategoryID { get; set; }
        public string CType { get; set; }
        public int BooksAmount { get; set; }
    }
}