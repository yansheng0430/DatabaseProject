using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BookStore.Models;
using System.Web.Configuration;

namespace BookStore.DAO
{
    public class BooksDAO
    {
        //搜尋出所有上架書籍
        public List<Book> GetAllBooks()
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                //連接預設為window驗證 假若需SQL驗證需要額外寫
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string queryString = "EXEC GetAllBooks";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以ISBN當作關鍵字的書籍(Search key word)
        public List<Book> GetBooksByISBNKeyWord(string ISBN)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetBooksByISBNKeyWord @ISBN";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters["@ISBN"].Value = ISBN;
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以Name當作關鍵字的書籍(Search key word)
        public List<Book> GetBooksByNameKeyWord(string name)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetBooksByNameKeyWord @BName";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@BName", SqlDbType.NVarChar));
                    command.Parameters["@BName"].Value = name;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以Author當作關鍵字的書籍(Search key word)
        public List<Book> GetBooksByAuthorKeyWord(string author)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetBooksByAuthorKeyWord @Author";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@Author", SqlDbType.NVarChar));
                    command.Parameters["@Author"].Value = author;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以Publisher當作關鍵字的書籍(Search key word)
        public List<Book> GetBooksByPublisherKeyWord(string publisher)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetBooksByPublisherKeyWord @Publisher";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@Publisher", SqlDbType.NVarChar));
                    command.Parameters["@Publisher"].Value = publisher;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以Category當作過濾器的書籍(Search Filter)
        public List<Book> GetBooksByCategoryFilter(string category)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetBooksByCategoryFilter @Category";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar));
                    command.Parameters["@Category"].Value = category;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以Price區間當作過濾器的書籍(Search Filter)
        public List<Book> GetBooksByPriceFilter(int lowerPrice, int higherPrice)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetBooksByPriceFilter @lowerPrice, @higherPrice";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@lowerPrice", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@higherPrice", SqlDbType.Int));
                    command.Parameters["@lowerPrice"].Value = lowerPrice;
                    command.Parameters["@higherPrice"].Value = higherPrice;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.ISBN = reader.GetString(0);
                        book.Name = reader.GetString(1);
                        book.UnitPrice = reader.GetInt32(2);
                        book.Quantity = reader.GetInt32(3);
                        book.Author = reader.GetString(4);
                        book.Category = reader.GetString(5);
                        book.Publisher = reader.GetString(6);
                        book.PublishDate = reader.GetDateTime(7);
                        book.Description = reader.GetString(8);
                        book.Cover = reader.GetString(9);
                        booksList.Add(book);
                    }
                    reader.Close();
                    return booksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //建立新書 Publisher和Category需要以combobox呈現且需要新增兩個增加按鈕
        public void CreateBook(Book newBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CreateNewBook @ISBN, @BName, @UnitPrice, @Quantity,  @Author," +
                                       "@Category, @Publisher, @PublishDate, @BDescription, @Cover";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@BName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Author", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Publisher", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PublishDate", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@BDescription", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Cover", SqlDbType.NVarChar));
                    command.Parameters["@ISBN"].Value = newBook.ISBN;
                    command.Parameters["@BName"].Value = newBook.Name;
                    command.Parameters["@UnitPrice"].Value = newBook.UnitPrice;
                    command.Parameters["@Quantity"].Value = newBook.Quantity;
                    command.Parameters["@Author"].Value = newBook.Author;
                    command.Parameters["@Category"].Value = newBook.Category;
                    command.Parameters["@Publisher"].Value = newBook.Publisher;
                    command.Parameters["@PublishDate"].Value = newBook.PublishDate.Date;
                    command.Parameters["@BDescription"].Value = newBook.Description;
                    command.Parameters["@Cover"].Value = newBook.Cover;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        //對書籍庫存上貨下貨
        //下貨數量的成功與否判斷要在CONTROL裡面做
        public void ManageBooks(BookManaged selectedBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                { 
                    string sqlString = "EXEC ManageBook @EmployeeID, @ISBN, @Quantity, @ActionState";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@ActionState", SqlDbType.NVarChar));
                    command.Parameters["@EmployeeID"].Value = selectedBook.EmployeeID;
                    command.Parameters["@ISBN"].Value = selectedBook.ISBN;
                    command.Parameters["@Quantity"].Value = selectedBook.Quantity;
                    command.Parameters["@ActionState"].Value = selectedBook.ActionState;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        //刪除書籍
        //是否允許刪除書籍要在Control裡面判斷庫存是否為空
        public void DeleteBook(string ISBN)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    string sqlString = "EXEC DeleteBook @ISBN";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters["@ISBN"].Value = ISBN;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //編輯書籍基本資料(HttpPost)
        public void EditBook(Book selectedbook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC EditBook @ISBN, @BName, @UnitPrice, @Author," +
                                       "@Category, @Publisher, @PublishDate, @BDescription, @Cover";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@BName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Author", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Publisher", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PublishDate", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@BDescription", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Cover", SqlDbType.NVarChar));
                    command.Parameters["@ISBN"].Value = selectedbook.ISBN;
                    command.Parameters["@BName"].Value = selectedbook.Name;
                    command.Parameters["@UnitPrice"].Value = selectedbook.UnitPrice;
                    command.Parameters["@Author"].Value = selectedbook.Author;
                    command.Parameters["@Category"].Value = selectedbook.Category;
                    command.Parameters["@Publisher"].Value = selectedbook.Publisher;
                    command.Parameters["@PublishDate"].Value = selectedbook.PublishDate.Date;
                    command.Parameters["@BDescription"].Value = selectedbook.Description;
                    command.Parameters["@Cover"].Value = selectedbook.Cover;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}