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

        public Employee GetEmployeeByEmployeeID(string employeeID)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetEmployeeByEmployeeID @EmployeeID";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
                    command.Parameters["@EmployeeID"].Value = employeeID;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = reader.GetString(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.CellPhone = reader.GetString(3);
                        employee.Address = reader.GetString(4);
                        employee.Email = reader.GetString(5);
                        employee.Account = reader.GetString(6);
                        employee.Password = reader.GetString(7);
                        employee.Office = reader.GetString(8);
                        reader.Close();
                        return employee;
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

        public void SaveEmployeeInformation(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC SaveEmployeeInformation @EmployeeID, @FirstName, @LastName, @CellPhone, @Address, @Email, @Password, @Office";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@CellPhone", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Office", SqlDbType.NVarChar));
                    command.Parameters["@EmployeeID"].Value = employee.EmployeeID;
                    command.Parameters["@FirstName"].Value = employee.FirstName;
                    command.Parameters["@LastName"].Value = employee.LastName;
                    command.Parameters["@CellPhone"].Value = employee.CellPhone;
                    command.Parameters["@Address"].Value = employee.Address;
                    command.Parameters["@Email"].Value = employee.Email;
                    command.Parameters["@Password"].Value = employee.Password;
                    command.Parameters["@Office"].Value = employee.Office;
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