using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using TemplateTest.Models;

namespace TemplateTest.DAO
{
    public class CustomerDAO
    {
        public Customer GetCustomerByAuthentication(AuthMember authMember)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    //string sqlString = "EXEC AuthenticateMember @Account @Password";
                    string sqlString = "select * from CUSTOMER where Account = @Account AND Passward = @Password";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    command.Parameters["@Account"].Value = authMember.Account;
                    command.Parameters["@Password"].Value = authMember.Password;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Customer authCustomer = new Customer();
                        authCustomer.CustomerID = reader.GetString(0);
                        authCustomer.FirstName = reader.GetString(1);
                        authCustomer.LastName = reader.GetString(2);
                        authCustomer.Sex = reader.GetBoolean(3);
                        authCustomer.CellPhone = reader.GetString(4);
                        authCustomer.Address = reader.GetString(5);
                        authCustomer.Email = reader.GetString(6);
                        authCustomer.Account = reader.GetString(7);
                        authCustomer.Password = reader.GetString(8);
                        reader.Close();
                        return authCustomer;
                    }
                    reader.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
    }
}