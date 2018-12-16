using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace BookStore.DAO
{
    public class PublishersDAO
    {
        //獲得所有的出版商資訊
        public List<Publisher> GetAllPublisher()
        {
            List<Publisher> publishersList = new List<Publisher>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC　GetAllPublishers";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Publisher publisher = new Publisher();
                        publisher.PublisherID = reader.GetString(0);
                        publisher.PName = reader.GetString(1);
                        publisher.Phone = reader.GetString(2);
                        publisher.PAddress = reader.GetString(3);
                        publishersList.Add(publisher);
                    }
                    reader.Close();
                    return publishersList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //獲得以出版商名字為篩選的出版商資訊(只會有一個輸出結果)
        public Publisher GetPublisherByName(string name)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC　GetPublisherByName @PName";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@PName", SqlDbType.NVarChar));
                    command.Parameters["@PName"].Value = name;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Publisher publisher = new Publisher();
                    publisher.PublisherID = reader.GetString(0);
                    publisher.PName = reader.GetString(1);
                    publisher.Phone = reader.GetString(2);
                    publisher.PAddress = reader.GetString(3);
                    reader.Close();
                    return publisher;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //新增出版商
        public void CreateNewPublisher(Publisher newPublisher)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CreateNewPublisher @PublisherID, @PName, @Phone, @PAddress";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@PublisherID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PAddress", SqlDbType.NVarChar));
                    command.Parameters["@PublisherID"].Value = newPublisher.PublisherID;
                    command.Parameters["@PName"].Value = newPublisher.PName;
                    command.Parameters["@Phone"].Value = newPublisher.Phone;
                    command.Parameters["@PAddress"].Value = newPublisher.PAddress;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //編輯出版商
        public void EditPublisher(Publisher selectedPublisher)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC EditPublisher @PublisherID, @PName, @Phone, @PAddress";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@PublisherID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PAddress", SqlDbType.NVarChar));
                    command.Parameters["@PublisherID"].Value = selectedPublisher.PublisherID;
                    command.Parameters["@PName"].Value = selectedPublisher.PName;
                    command.Parameters["@Phone"].Value = selectedPublisher.Phone;
                    command.Parameters["@PAddress"].Value = selectedPublisher.PAddress;
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