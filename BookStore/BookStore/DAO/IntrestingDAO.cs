using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using BookStore.Models;

namespace BookStore.DAO
{
    public class IntrestingDAO
    {
        //增加有興趣的書籍
        public void AddIntrestingBook(Intresting intrestingBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC AddIntrestingBook @CustomerID, @ISBN, @AddDate";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = intrestingBook.CustomerID;
                    command.Parameters["@ISBN"].Value = intrestingBook.ISBN;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //刪除有興趣的書籍
        public void DeleteIntrestingBook(Intresting intrestingBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC DeleteIntrestingBook @CustomerID, @ISBN";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = intrestingBook.CustomerID;
                    command.Parameters["@ISBN"].Value = intrestingBook.ISBN;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //回傳所有有興趣的商品
        public List<Book> GetIntrestingBooksByCustomerID(string customerID)
        {
            List<Book> intrestingBooksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetIntrestingBooksByCustomerID @CustomerID";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = customerID;
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
                        intrestingBooksList.Add(book);
                    }
                    reader.Close();
                    return intrestingBooksList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}