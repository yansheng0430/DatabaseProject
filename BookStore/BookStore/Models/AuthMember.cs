using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BookStore.Authentication;

namespace BookStore.Models
{
    public class AuthMember : IValidatableObject
    {
        private string _identity;

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public string PublicKey { get; set; }

        [Required]
        public string Identity
        {
            get
            {
                return _identity;
            }
            set
            {
                Account = MvcApplication._RSAHelper.Encrypt(MvcApplication._RSADataList.nowPublicKey, Account);
                Password = MvcApplication._RSAHelper.Encrypt(MvcApplication._RSADataList.nowPublicKey, Password);
                _identity = value;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            FormValueCondition fvc = new FormValueCondition();
            if (Account.Length < 3)
                yield return new ValidationResult("The lenth of Account cannot be less than 3 !", new string[] { "Account" });
            if (Password.Length < 3)
                yield return new ValidationResult("The lengtj of Password cannot be less than 3 !", new string[] { "Password" }); ;
            if (!fvc.IsOnlyNumberAndEG(Account))
                yield return new ValidationResult("Only Number And English !!", new string[] { "Account" });
            if (!fvc.IsOnlyNumberAndEG(Password))
                yield return new ValidationResult("Only Number And English !!", new string[] { "Password" });
        }
    }
}