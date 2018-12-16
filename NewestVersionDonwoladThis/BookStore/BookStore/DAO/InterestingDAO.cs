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
    public class InterestingDAO
    {
        //增加有興趣的書籍
        public void AddIntrestingBook(Interesting intrestingBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC AddIntrestingBook @CustomerID, @ISBN";
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
        public void DeleteIntrestingBook(Interesting intrestingBook)
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
        public List<Interesting> GetIntrestingBooksByCustomerID(string customerID)
        {
            List<Interesting> intrestingList = new List<Interesting>();
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
                        Interesting interestingBook = new Interesting();
                        interestingBook.CustomerID = reader.GetString(0);
                        interestingBook.ISBN = reader.GetString(1);
                        interestingBook.Name = reader.GetString(2);
                        interestingBook.Price = reader.GetInt32(3);
                        interestingBook.Cover = reader.GetString(4);
                        interestingBook.AddDate = reader.GetDateTime(5);
                        intrestingList.Add(interestingBook);
                    }
                    reader.Close();
                    return intrestingList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}