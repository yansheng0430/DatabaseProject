using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Employee
    {
            public string EmployeeID { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [StringLength(20)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(20)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Cell Phone")]
            [StringLength(10)]
            [Phone]
            public string CellPhone { get; set; }

            [Required]
            [StringLength(50)]
            public string Address { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(50)]
            public string Email { get; set; }

            [Required]
            [StringLength(50)]
            public string Account { get; set; }

            [Required]
            [StringLength(50)]
            public string Password { get; set; }

            [Required]
            public string Office { get; set; }

            public string Roles { get; set; }

    }
}