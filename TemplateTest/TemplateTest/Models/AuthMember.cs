using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TemplateTest.Models
{
    public class AuthMember //: IValidatableObject
    {
        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

        }*/
    }
}