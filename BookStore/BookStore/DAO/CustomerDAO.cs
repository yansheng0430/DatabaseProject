﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using BookStore.Models;

namespace BookStore.DAO
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
                    string sqlString = "EXEC AuthenticateCustomer @Account, @Password";
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

        public Customer GetCustomerByCustomerID(string customerID)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetCustomerByCustomerID @CustomerID";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = customerID;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = reader.GetString(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.Sex = reader.GetBoolean(3);
                        customer.CellPhone = reader.GetString(4);
                        customer.Address = reader.GetString(5);
                        customer.Email = reader.GetString(6);
                        customer.Account = reader.GetString(7);
                        customer.Password = reader.GetString(8);
                        reader.Close();
                        return customer;
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

        public void SaveCustomerInformation(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC SaveCustomerInformation @CustomerID, @FirstName, @LastName, @CellPhone, @Address, @Email, @Password";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@CellPhone", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    command.Parameters["@CustomerID"].Value = customer.CustomerID;
                    command.Parameters["@FirstName"].Value = customer.FirstName;
                    command.Parameters["@LastName"].Value = customer.LastName;
                    command.Parameters["@CellPhone"].Value = customer.CellPhone;
                    command.Parameters["@Address"].Value = customer.Address;
                    command.Parameters["@Email"].Value = customer.Email;
                    command.Parameters["@Password"].Value = customer.Password;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public bool CheckCustomerAccountExits(string account)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CheckCustomerAccountExits @Account";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar));
                    command.Parameters["@Account"].Value = account;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
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

        public void CreateCustomer(Customer newCustomer)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CreateCustomer @FirstName, @LastName, @Sex, @CellPhone, @CAddress, @Email, @Account, @Password";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Sex", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@CellPhone", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@CAddress", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    command.Parameters["@FirstName"].Value = newCustomer.FirstName;
                    command.Parameters["@LastName"].Value = newCustomer.LastName;
                    command.Parameters["@Sex"].Value = newCustomer.Sex;
                    command.Parameters["@CellPhone"].Value = newCustomer.CellPhone;
                    command.Parameters["@CAddress"].Value = newCustomer.Address;
                    command.Parameters["@Email"].Value = newCustomer.Email;
                    command.Parameters["@Account"].Value = newCustomer.Account;
                    command.Parameters["@Password"].Value = newCustomer.Password;
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