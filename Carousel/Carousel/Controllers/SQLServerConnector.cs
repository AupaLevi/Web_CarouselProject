using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Carousel.Models;

namespace Carousel.Controllers
{
    public class SQLServerConnector 
    {
        private SqlConnection sqlConnection;
        private string actionResult;

        public SQLServerConnector()
        {
            Initializer();
        }

        private void Initializer()
        {
            SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder();
            Builder.DataSource = "192.168.168.207\\SQLEXPRESS02";
            Builder.InitialCatalog = "aupaweb_base";
            Builder.UserID = "sa";
            Builder.Password = "#Aupa=234";
            String sqlConnectionString = Builder.ConnectionString;
            sqlConnection = new SqlConnection(sqlConnectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                sqlConnection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 53:
                        break;
                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                sqlConnection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.Message);
                Console.Write("MySqlException :" + ex.Message);
                return false;
            }
        }
        public String InsertPostData(PostDataCarousel postdatacarousel)
        {
            String sqlString = "INSERT INTO aaz_file ( " +
                                     " aaz01, aaz02, aaz03, aaz05  " +
                                     ") VALUES ( " +
                                     " @val01, @val02, @val03, @val05   " +
                                     ")";
            OpenConnection();
            actionResult = "SUCCESS";

            try
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlString;

                sqlCommand.Parameters.AddWithValue("@val01", postdatacarousel.Aaz01);
                sqlCommand.Parameters.AddWithValue("@val02", postdatacarousel.Aaz02);
                sqlCommand.Parameters.AddWithValue("@val03", postdatacarousel.Aaz03);
                sqlCommand.Parameters.AddWithValue("@val05", postdatacarousel.Aaz05);


                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                actionResult = "FAIL" + ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return actionResult;
        }//End of Insert Into

        public List<PostDataCarousel> getPostsList()
        {
            String sqlString = "SELECT * FROM aaz_file" +
                                         " ORDER BY aaz01 DESC" +
                                         "";
            List<PostDataCarousel> postsList = new List<PostDataCarousel>();

            OpenConnection();
            actionResult = "SUCCESS";

            try
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlString;

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PostDataCarousel postdatacarousel = new PostDataCarousel();

                        postdatacarousel.Aaz01 = dataReader.GetString(dataReader.GetOrdinal("Aaz01"));
                        postdatacarousel.Aaz02 = dataReader.GetString(dataReader.GetOrdinal("Aaz02"));
                        postdatacarousel.Aaz03 = dataReader.GetString(dataReader.GetOrdinal("Aaz03"));
                        postdatacarousel.Aaz05 = dataReader.GetString(dataReader.GetOrdinal("Aaz05"));


                        postsList.Add(postdatacarousel);
                    }
                }
            }
            catch (Exception ex)
            {
                actionResult = "FAIL" + ex.Message;
            }
            finally
            {
                CloseConnection();
            }

            return postsList;
        }//End of getPostsList

        public List<PostDataCarousel> getTopPostsList(int num)
        {
            String sqlString = "SELECT TOP " + num +
                                        "  aaz01,aaz02,aaz03,aaz05" +
                                        "  FROM aaz_file" +
                                        "  ORDER BY aaz01 DESC" +
                                        "";
            List<PostDataCarousel> postsList = new List<PostDataCarousel>();

            OpenConnection();
            actionResult = "SUCCESS";

            try
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlString;

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PostDataCarousel postdatacarousel = new PostDataCarousel();

                        postdatacarousel.Aaz01 = dataReader.GetString(dataReader.GetOrdinal("Aaz01"));
                        postdatacarousel.Aaz02 = dataReader.GetString(dataReader.GetOrdinal("Aaz02"));
                        postdatacarousel.Aaz03 = dataReader.GetString(dataReader.GetOrdinal("Aaz03"));
                        postdatacarousel.Aaz05 = dataReader.GetString(dataReader.GetOrdinal("Aaz05"));


                        postsList.Add(postdatacarousel);
                    }
                }
            }
            catch (Exception ex)
            {
                string v = "FAIL" + ex.Message;
                actionResult = v;
            }
            finally
            {
                CloseConnection();
            }

            return postsList;
        }//End of getPostsList

        public List<PostDataCarousel> getPostsListOnDemand(String sqlCriteria)
        {
            String sqlString = "SELECT * FROM aaz_file WHERE " + sqlCriteria;
            List<PostDataCarousel> postsList = new List<PostDataCarousel>();

            OpenConnection();
            actionResult = "SUCCESS";
            try
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlString;

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PostDataCarousel postdatacarousel = new PostDataCarousel();

                        postdatacarousel.Aaz01 = dataReader.GetString(dataReader.GetOrdinal("Aaz01"));
                        postdatacarousel.Aaz02 = dataReader.GetString(dataReader.GetOrdinal("Aaz02"));
                        postdatacarousel.Aaz03 = dataReader.GetString(dataReader.GetOrdinal("Aaz03"));
                        postdatacarousel.Aaz05 = dataReader.GetString(dataReader.GetOrdinal("Aaz05"));


                        postsList.Add(postdatacarousel);
                    }
                }
            }
            catch (Exception ex)
            {
                actionResult = "FAIL" + ex.Message;
            }
            finally
            {
                CloseConnection();
            }

            return postsList;
        }

        public String ConfirmedDelete(String postID)
        {
            String sqlString = "DELETE FROM aaz_file WHERE aaz01 = '" + postID + "'";
            int deletedRows;
            actionResult = "SUCCESS";
            try
            {
                OpenConnection();

                SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
                deletedRows = sqlCommand.ExecuteNonQuery();
                if (deletedRows == 0)
                {
                    actionResult = "FAIL";
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection();
            }

            return actionResult;
        }

        public String ConfirmedEdit(PostDataCarousel postdatacarousel)
        {
            String sqlString = " UPDATE  aaz_file " +
                                         " SET aaz02 =  @val01,"  +  "  aaz03 = @val02  " +  
                                         " WHERE aaz01 = @val03" +
                                         "";




            actionResult = "SUCCESS";

            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);




                sqlCommand.Parameters.AddWithValue("@val01", postdatacarousel.Aaz02);
                sqlCommand.Parameters.AddWithValue("@val02", postdatacarousel.Aaz03);
                sqlCommand.Parameters.AddWithValue("@val03", postdatacarousel.Aaz01);
                


                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                actionResult = "FAIL" + ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return actionResult;
        }
    }
}
