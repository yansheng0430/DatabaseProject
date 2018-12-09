using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace TemplateTest.Models
{
    public class Customer
    {
        [Required]
        public string CustomerID{ get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Sex { get; set; }

        [Required]
        public string CellPhone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}