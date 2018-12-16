using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class AuthMember : IValidatableObject
    {
        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Account.Length < 3)
                yield return new ValidationResult("The lenth of Account cannot be less than 3 !", new string[] { "Account" });
            if (Password.Length < 3)
                yield return new ValidationResult("The lengtj of Password cannot be less than 3 !", new string[] { "Password" }); ;
        }
    }
}