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
        // Get: Shop
        public ActionResult ShopFilterCategory(string category)
        {
            CategoriesDAO categoriesDAO = new CategoriesDAO();
            BooksDAO booksDAO = new BooksDAO();
            ViewBag.AllCategories = categoriesDAO.GetAllCategories();
            ViewBag.AllBooks = booksDAO.GetBooksByCategoryFilter(category);
            return View("Shop");
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
            if (ModelState.IsValid)
            {
                CustomerDAO customerDAO = new CustomerDAO();
                customerDAO.CreateCustomer(newCustomer);
                TempData["CreateResult"] = "Create Successfully!";
                return RedirectToAction("AccountCreate", "Customer");
            }
            else
            {
                TempData["CreateResult"] = "Create Unsuccessfully!";
                return RedirectToAction("AccountCreate", "Customer");
            }
            
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
        public ActionResult ShoppingCart(string customerID)
        {
            ShoppingCartDAO shoppingCartDAO = new ShoppingCartDAO();
            List<BookShopped> shoppingCartList = shoppingCartDAO.GetShoppingCartByCustomerID(customerID);
            int total = 0;
            foreach (BookShopped bs in shoppingCartList)
            {
                total += bs.Subtotal;
            }
            ViewBag.BooksTotal = total;
            return View(shoppingCartList);
        }

        [Authorize]
        //Get:ShoppingCart
        public ActionResult ShoppingCartAdded(string customerID, string ISBN, int quantity)
        {
            ShoppingCartBook shoppingBook = new ShoppingCartBook();
            ShoppingCartDAO shoppingCartDAO = new ShoppingCartDAO();
            shoppingBook.CustomerID = customerID;
            shoppingBook.ISBN = ISBN;
            shoppingBook.Amount = quantity;
            shoppingCartDAO.AddShoppingCartBook(shoppingBook);
            return RedirectToAction("ShoppingCart", new { customerID = customerID });
        }

        [Authorize]
        //Get:ShoppingCart
        public ActionResult ShoppingCartDeleted(string customerID, string ISBN)
        {
            ShoppingCartBook shoppingBook = new ShoppingCartBook();
            ShoppingCartDAO shoppingCartDAO = new ShoppingCartDAO();
            shoppingBook.CustomerID = customerID;
            shoppingBook.ISBN = ISBN;
            shoppingCartDAO.DeleteShoppingCart(customerID, ISBN);
            return RedirectToAction("ShoppingCart", new { customerID = customerID });
        }

        [Authorize]
        //Get:Checkout
        public ActionResult Checkout(string customerID)
        {
            ShoppingCartDAO shoppingCartDAO = new ShoppingCartDAO();
            List<BookShopped> shoppingCartList = shoppingCartDAO.GetShoppingCartByCustomerID(customerID);
            int total = 0;
            foreach (BookShopped bs in shoppingCartList)
            {
                total += bs.Subtotal;
            }
            ViewBag.ShoppingCartList = shoppingCartList;
            ViewBag.BooksTotal = total;
            return View();
        }

        [Authorize]
        [HttpPost]
        //Get:Checkout
        public ActionResult Checkout(Order newOrder)
        {
            OrdersDAO ordersDAO = new OrdersDAO();
            ordersDAO.CreateNewOrder(newOrder);
            return RedirectToAction("Thank");
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