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
    public class ShoppingCartDAO
    {
        //將書加入購物車內
        public void AddShoppingCartBook(ShoppingCartBook shoppingBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC AddShoppingCartBook @CustomerID, @ISBN, @Amount";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                    command.Parameters["@CustomerID"].Value = shoppingBook.CustomerID;
                    command.Parameters["@ISBN"].Value = shoppingBook.ISBN;
                    command.Parameters["Amount"].Value = shoppingBook.Amount;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //改變購物車內要購買的數量
        public void ChangeShoppingCartBookAmount(ShoppingCartBook shoppingBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC ChangeShoppingCartBookAmount @CustomerID, @ISBN, @Amount";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                    command.Parameters["@CustomerID"].Value = shoppingBook.CustomerID;
                    command.Parameters["@ISBN"].Value = shoppingBook.ISBN;
                    command.Parameters["Amount"].Value = shoppingBook.Amount;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        //清空購物車的所有東西
        public void ClearShoppingCart(string customerID)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC ClearShoppingCart @CustomerID";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = customerID;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //確認要加入購物車的商品原本是否就在購物車 true存在 false 不存在
        public bool CheckShoppingCartBookExist(ShoppingCartBook shoppingBook)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CheckShoppingCartBookExist @CustomerID, @ISBN";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = shoppingBook.CustomerID;
                    command.Parameters["@ISBN"].Value = shoppingBook.ISBN;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    int count = reader.GetInt32(0);
                    reader.Close();
                    if (count != 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /* 購物車跟網頁互動有些問題
        //獲得購物車內所有的商品資訊和小計
        public List<BookShopped> GetShoppingCart(string customerID)
        {
            List<BookShopped> bookShoppedList = new List<BookShopped>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetShoppingCart @CustomerID";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = customerID;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        BookShopped bookShopped = new BookShopped();
                        bookShopped.ISBN = reader.GetString(0);
                        bookShopped.Name = reader.GetName(1);
                        bookShopped.UnitPrice = reader.GetInt32(2);
                        bookShopped.Amount = reader.GetInt32(3);
                        bookShopped.Cover = reader.GetString(4);
                        bookShoppedList.Add(bookShopped);
                    }
                    reader.Close();
                    return bookShoppedList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }*/
    }
}