using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BookStore.Authentication;
using BookStore.DAO;

namespace BookStore.Models
{
    public class BookManaged : IValidatableObject
    {
        public string EmployeeID { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ActionState { get; set; }    
        public string Cover { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            BooksDAO booksDAO = new BooksDAO();
            FormValueCondition fvc = new FormValueCondition();

            if (Quantity <= 0 | (!fvc.IsOnlyNumber(Convert.ToString(Quantity))))
                yield return new ValidationResult("Please enter correct number !!", new string[]{ "Quantity" });
            
            if (ActionState == "Decrease" & (booksDAO.GetBooksByISBNKeyWord(ISBN)[0].Quantity < Quantity))
                yield return new ValidationResult("Please enter correct number !!", new string[] { "Quantity" });
        }
    }
}