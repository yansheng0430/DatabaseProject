using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DAO;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: EmployeeHome
        public ActionResult EmployeeHome()
        {
            return View();
        }

        //GET: BookList
        public ActionResult BooksList()
        {
            BooksDAO bookDB = new BooksDAO();
            List<Book> AllBooks = bookDB.GetAllBooks();
            return View(AllBooks);
        }

        //GET: CreateBooks
        public ActionResult CreateBooks()
        {
            return View();
        }

        //POST: CreateBooks
        [HttpPost]
        public ActionResult CreateBooks(Book newBook)
        {
            if (ModelState.IsValid)
            {
                BooksDAO booksDB = new BooksDAO();
                booksDB.CreateBook(newBook);
                TempData["CreateResult"] = String.Format("NewBook {0} was Added Successfully", newBook.Name);
                return RedirectToAction("CreateBooks");
            }
            else
            {
                TempData["CreateResult"] = String.Format("NewBook {0} was Added Unsuccessfully", newBook.Name);
                return View(newBook);
            }
        }
    }
}