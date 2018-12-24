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
        [Authorize(Roles = "Admin")]
        // GET: Employee
        public ActionResult ProductCreate()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ProductCreate(Book book)
        {
            if (ModelState.IsValid)
            {
                BooksDAO booksDAO = new BooksDAO();
                booksDAO.CreateBook(book);
                return RedirectToAction("ProductList", "Employee");
            }
            else
            {
                TempData["CommandMessage"] = "Create Unsuccessfully!!";
                return View(book);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductEdit(string ISBN)
        {
            BooksDAO booksDAO = new BooksDAO();
            List<Book> booksList = booksDAO.GetBooksByISBNKeyWord(ISBN);
            Book bookEdited = booksList[0];
            return View(bookEdited);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ProductEdit(Book book)
        {
            try
            {
                BooksDAO booksDAO = new BooksDAO();
                booksDAO.EditBook(book);
                return RedirectToAction("ProductList", "Employee");
            }
            catch
            {
                TempData["CommandMessage"] = "Edit Unsuccessfully!!";
                return View(book);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductManage(string EmployeeID, string ISBN)
        {

            BooksDAO booksDAO = new BooksDAO();
            List<Book> booksList = booksDAO.GetBooksByISBNKeyWord(ISBN);
            BookManaged bookManaged = new BookManaged();
            bookManaged.EmployeeID = EmployeeID;
            bookManaged.ISBN = booksList[0].ISBN;
            bookManaged.Name = booksList[0].Name;
            bookManaged.Quantity = booksList[0].Quantity;
            bookManaged.Cover = booksList[0].Cover;
            return View(bookManaged);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ProductManage(BookManaged bookManaged)
        {
            BooksDAO booksDAO = new BooksDAO();
            if (ModelState.IsValid)
            {
                booksDAO.ManageBooks(bookManaged);
                return RedirectToAction("ProductList", "Employee");
            }
            else
            {
                TempData["CommandMessage"] = "Manage Unsuccessfully!!";
                List<Book> booksList = booksDAO.GetBooksByISBNKeyWord(bookManaged.ISBN);
                BookManaged oldBookManaged = new BookManaged();
                oldBookManaged.EmployeeID = User.Identity.Name;
                oldBookManaged.ISBN = booksList[0].ISBN;
                oldBookManaged.Name = booksList[0].Name;
                oldBookManaged.Quantity = booksList[0].Quantity;
                oldBookManaged.Cover = booksList[0].Cover;
                return View(oldBookManaged);
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Employee
        public ActionResult ProductDelete(string ISBN)
        {
            try
            {
                BooksDAO booksDAO = new BooksDAO();
                booksDAO.DeleteBook(ISBN);
            }
            catch
            {
                TempData["ErrorMessage"] = "Users are using!!";
            }
            return RedirectToAction("ProductList", "Employee");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ProductSearchByKey(FormCollection searchFormCollection)
        {
            string selectKey = searchFormCollection["bookKeySelector"];
            string keyValue = searchFormCollection["searchKeyText"];
            List<Book> booksList = new List<Book>();
            BooksDAO booksDAO = new BooksDAO();
            CategoriesDAO categoriesDAO = new CategoriesDAO();
            if (selectKey == "Book")
                booksList = booksDAO.GetBooksByNameKeyWord(keyValue);
            else if (selectKey == "Author")
                booksList = booksDAO.GetBooksByAuthorKeyWord(keyValue);
            else if (selectKey == "Publisher")
                booksList = booksDAO.GetBooksByPublisherKeyWord(keyValue);
            else if (selectKey == "ISBN")
                booksList = booksDAO.GetBooksByISBNKeyWord(keyValue);
            return View("ProductList", booksList);
        }

        [Authorize(Roles = "Admin")]
        // GET: Employee
        public ActionResult ProductList()
        {
            BooksDAO bookDAO = new BooksDAO();
            List<Book> booksList = bookDAO.GetAllBooks();
            return View(booksList);
        }

        [Authorize(Roles = "Admin")]
        // GET: Employee
        public ActionResult EmployeeInfo(string employeeID)
        {
            EmployeeDAO employeeDAO = new EmployeeDAO();
            Employee employee = employeeDAO.GetEmployeeByEmployeeID(employeeID);
            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        // GET: Employee
        public ActionResult EmployeeInfo(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeDAO employeeDAO = new EmployeeDAO();
                employeeDAO.SaveEmployeeInformation(employee);
                TempData["CommandMessage"] = "Edit Successfully !!";
            }
            else
            {
                TempData["CommandMessage"] = "Edit Unsuccessfully !!";
            }
            return View(employee);
        }
    }
}