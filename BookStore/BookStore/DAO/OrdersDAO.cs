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
    public class OrdersDAO
    {
        public void CreateNewOrder(Order newOrder)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CreateNewOrder @CustomerID, @CreditCard, @Phone, @Address, @Email, @FirstName, @LastName";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@CreditCard", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = newOrder.CustomerID;
                    command.Parameters["@CreditCard"].Value = newOrder.CreditCard;
                    command.Parameters["@Phone"].Value = newOrder.Phone;
                    command.Parameters["@Address"].Value = newOrder.Address;
                    command.Parameters["@Email"].Value = newOrder.Email;
                    command.Parameters["@FirstName"].Value = newOrder.FirstName;
                    command.Parameters["@LastName"].Value = newOrder.LastName;
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