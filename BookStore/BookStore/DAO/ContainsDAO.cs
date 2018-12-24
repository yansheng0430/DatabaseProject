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
    public class ContainsDAO
    {
        public List<Contain> GetContainsByOrderID(string orderID)
        {
            List<Contain> containsList = new List<Contain>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC GetContainsByOrderID @OrderID";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.NVarChar));
                    command.Parameters["@OrderID"].Value = orderID;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contain contain = new Contain();
                        contain.ISBN = reader.GetString(0);
                        contain.Name = reader.GetString(1);
                        contain.Price = reader.GetInt32(2);
                        contain.Quantity = reader.GetInt32(3);
                        containsList.Add(contain);
                    }
                    reader.Close();
                    return containsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}