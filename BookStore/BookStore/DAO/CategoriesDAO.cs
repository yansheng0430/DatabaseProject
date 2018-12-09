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
    public class CategoriesDAO
    {
        //取得所有Category的名稱和它所包含的書本數量
        public List<Category> GetAllCategories()
        {
            List<Category> categoriesList = new List<Category>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC　GetAllCategories";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = reader.GetString(0);
                        category.CType = reader.GetString(1);
                        category.BooksAmount = reader.GetInt32(2);
                        categoriesList.Add(category);
                    }
                    reader.Close();
                    return categoriesList;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        //新增種類
        public void CreateCategory(Category newCategory)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC CreateNewCategory @CategoryID, @CType";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@CType", SqlDbType.NVarChar));
                    command.Parameters["@CategoryID"].Value = newCategory.CategoryID;
                    command.Parameters["@CType"].Value = newCategory.CType;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        //編輯種類
        public void EditCategory(Category selectedCategory)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
                try
                {
                    connection.Open();
                    string sqlString = "EXEC EditCategory @CategoryID, @CType";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@CType", SqlDbType.NVarChar));
                    command.Parameters["@CategoryID"].Value = selectedCategory.CategoryID;
                    command.Parameters["@CType"].Value = selectedCategory.CType;
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