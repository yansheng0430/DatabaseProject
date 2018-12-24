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
        // Get: Shop
        public ActionResult ShopFilterPrice(FormCollection priceFormCollection)
        {
            int lowPrice = int.Parse(priceFormCollection["lowPrice"]);
            int highPrice = int.Parse(priceFormCollection["highPrice"]);
            CategoriesDAO categoriesDAO = new CategoriesDAO();
            BooksDAO booksDAO = new BooksDAO();
            ViewBag.AllCategories = categoriesDAO.GetAllCategories();
            ViewBag.AllBooks = booksDAO.GetBooksByPriceFilter(lowPrice, highPrice);
            return View("Shop");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ShopSearchByKey(FormCollection searchFormCollection)
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
            ViewBag.AllCategories = categoriesDAO.GetAllCategories();
            ViewBag.AllBooks = booksList;
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

        [Authorize(Roles = "User")]
        //Get:MemberInformation
        public ActionResult CustomerInfo(string customerID)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            Customer customer = customerDAO.GetCustomerByCustomerID(customerID);
            return View(customer);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult CustomerInfo(Customer customer)
        {
            if (ModelState.IsValid)
            {
                CustomerDAO customerDAO = new CustomerDAO();
                customerDAO.SaveCustomerInformation(customer);
                TempData["CommandMessage"] = "Edit Successfully !!";
            }
            else
            {
                TempData["CommandMessage"] = "Edit Unsuccessfully !!";
            }
            return View(customer);
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
                if (customerDAO.CheckCustomerAccountExits(newCustomer.Account))
                {
                    TempData["CreateResult"] = "Account Exits!";
                }
                else
                {
                    customerDAO.CreateCustomer(newCustomer);
                    TempData["CreateResult"] = "Create Successfully!";
                }
                return View();
            }
            else
            {
                return View(newCustomer);
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

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
        [HttpPost]
        //Get:Checkout
        public ActionResult Checkout(Order newOrder)
        {
            OrdersDAO ordersDAO = new OrdersDAO();
            ordersDAO.CreateNewOrder(newOrder);
            return RedirectToAction("Thank");
        }

        [Authorize(Roles = "User")]
        //Get:Interested
        public ActionResult Interested(string customerID)
        {
            InterestingDAO interestingDAO = new InterestingDAO();
            List<Interesting> interestingList = interestingDAO.GetIntrestingBooksByCustomerID(customerID);
            return View(interestingList);
        }

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
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