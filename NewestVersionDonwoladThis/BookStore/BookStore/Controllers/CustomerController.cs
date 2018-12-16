using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookStore.Models;
using BookStore.DAO;

namespace BookStore.Controllers
{
    public class CustomerController : Controller
    {
        private AuthManager _authManager;

        public CustomerController()
        {
            _authManager = new AuthManager();
        }

        [AllowAnonymous]
        // GET: Index
        public ActionResult Index()
        { 
            return View();
        }

        [AllowAnonymous]
        // Get: Shop
        public ActionResult Shop()
        {
            CategoriesDAO categoriesDAO = new CategoriesDAO();
            BooksDAO booksDAO = new BooksDAO();
            ViewBag.AllCategories = categoriesDAO.GetAllCategories();
            ViewBag.AllBooks = booksDAO.GetAllBooks();
            return View();
        }

        [AllowAnonymous]
        //Get: About
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        //Get: Recommand
        public ActionResult Recommand()
        {
            BooksDAO booksDAO = new BooksDAO();
            List<Book> AllBooks = booksDAO.GetAllBooks();
            return View(AllBooks);
        }

        [AllowAnonymous]
        //Get: MemberSignIn
        public ActionResult MemberSignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult MemberSignIn(AuthMember authCustomer)
        {
            if (ModelState.IsValid)
            {
                if (_authManager.SignIn(authCustomer))
                    return RedirectToAction("Index", "Customer");
                else
                    return View(authCustomer);
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult MemberSignOut()
        {
            _authManager.SignOut();
            return RedirectToAction("Index", "Customer");
        }
        
        [Authorize]
        //Get:MemberInformation
        public ActionResult MemberInformation()
        {
            return View();
        }
      
        [AllowAnonymous]
        //Get:AccountCreate
        public ActionResult AccountCreate()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //Post:AccountCreate
        public ActionResult AccountCreate(Customer newCustomer)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            customerDAO.CreateCustomer(newCustomer);
            TempData["CreateResult"] = "Create Successfully!";
            return RedirectToAction("AccountCreate", "Customer");
        }

        [AllowAnonymous]
        //Get:ShopSingle
        public ActionResult ShopSingle(string ISBN)
        {
            BooksDAO booksDAO = new BooksDAO();
            List<Book> selectedBook = booksDAO.GetBooksByISBNKeyWord(ISBN);
            return View(selectedBook);
        }

        [Authorize]
        //Get:ShoppingCart
        public ActionResult ShoppingCart()
        {
            return View();
        }

        [Authorize]
        //Get:Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        //Get:Interested
        public ActionResult Interested(string customerID)
        {
            InterestingDAO interestingDAO = new InterestingDAO();
            List<Interesting> interestingList = interestingDAO.GetIntrestingBooksByCustomerID(customerID);
            return View(interestingList);
        }
        
        [Authorize]
        //Get:InterestedBookAdded
        //因為無法同名用其他名子去取代這個方法不僅可以跳轉到相對應有興趣頁面還可以同時增加有興趣書籍
        public ActionResult InterestedBookAdded(string customerID, string ISBN)
        {
            Interesting interestingBook = new Interesting();
            InterestingDAO interestingDAO = new InterestingDAO();
            interestingBook.CustomerID = customerID;
            interestingBook.ISBN = ISBN;
            interestingDAO.AddIntrestingBook(interestingBook);
            return RedirectToAction("Interested", new { customerID = customerID });
        }

        [Authorize]
        //Get:InterestedBookDeleted
        //因為無法同名用其他名子去取代這個方法不僅可以跳轉到相對應有興趣頁面還可以同時刪除有興趣書籍
        public ActionResult InterestedBookDeleted(string customerID, string ISBN)
        {
            Interesting interestingBook = new Interesting();
            InterestingDAO interestingDAO = new InterestingDAO();
            interestingBook.CustomerID = customerID;
            interestingBook.ISBN = ISBN;
            interestingDAO.DeleteIntrestingBook(interestingBook);
            return RedirectToAction("Interested", new { customerID = customerID });
        }

        [AllowAnonymous]
        //Get:Thank
        public ActionResult Thank()
        {
            return View();
        }

    }
}