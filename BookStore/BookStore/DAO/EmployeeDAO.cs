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
    public class EmployeeDAO
    {
        public Employee GetEmployeeByAuthentication(AuthMember authMember)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC AuthenticateEmployee @Account, @Password";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    command.Parameters["@Account"].Value = authMember.Account;
                    command.Parameters["@Password"].Value = authMember.Password;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Employee authEmployee = new Employee();
                        authEmployee.EmployeeID = reader.GetString(0);
                        authEmployee.FirstName = reader.GetString(1);
                        authEmployee.LastName = reader.GetString(2);
                        authEmployee.CellPhone = reader.GetString(3);
                        authEmployee.Address = reader.GetString(4);
                        authEmployee.Email = reader.GetString(5);
                        authEmployee.Account = reader.GetString(6);
                        authEmployee.Password = reader.GetString(7);
                        authEmployee.Office = reader.GetString(8);
                        reader.Close();
                        return authEmployee;
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