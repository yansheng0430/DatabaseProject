using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DAO;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class EmployeeController : Controller
    {
        [Authorize]
        // GET: Employee
        public ActionResult ProductCreate()
        {
            return View();
        }

        [Authorize]
        // GET: Employee
        public ActionResult ProductManage()
        {
            return View();
        }

        [Authorize]
        // GET: Employee
        public ActionResult ProductList()
        {
            BooksDAO bookDAO = new BooksDAO();
            List<Book> booksList = bookDAO.GetAllBooks();
            return View(booksList);
        }

        public ActionResult ProductDelete(string ISBN)
        {
            BooksDAO booksDAO = new BooksDAO();
            booksDAO.DeleteBook(ISBN);
            return RedirectToAction("ProductList", "Employee");
        }

        [Authorize]
        // GET: Employee
        public ActionResult EmployeeAccount()
        {
            return View();
        }
        
    }
}