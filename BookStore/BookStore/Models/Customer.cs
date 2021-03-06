﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using BookStore.DAO;
using BookStore.Authentication;

namespace BookStore.Models
{
    public class Customer : IValidatableObject
    {
        public string CustomerID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        public bool Sex { get; set; }

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

        public string Roles { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            FormValueCondition fvc = new FormValueCondition();

            if (Account.Length < 8)
                yield return new ValidationResult("This Account is too short !!", new string[] { "Account" });
            if (Password.Length < 8)
                yield return new ValidationResult("This Password is too shor !!", new string[] { "Password" });
            if (!fvc.IsOnlyNumberAndEG(Account))
                yield return new ValidationResult("Only Number And English !!", new string[] { "Account" });
            if (!fvc.IsOnlyNumberAndEG(Password))
                yield return new ValidationResult("Only Number And English !!", new string[] { "Password" });
        }
    }
}