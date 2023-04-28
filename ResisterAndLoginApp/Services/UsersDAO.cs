using static System.Net.Mime.MediaTypeNames;
using System;
using ResisterAndLoginApp.Models;
using System.Reflection.Metadata.Ecma335;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ResisterAndLoginApp.Services
{
    public class UsersDAO
    {
        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Test; " +
        //    "Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;" +
        //    "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=users;";

        public bool FindUserByNameAndPassword(UserModel user)
        {

            bool success = false;
            //string sqlStatement = "SELECT * FROM DBO.users WHERE username = @username AND password = @password";
   
            //command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 100).Value = user.UserName;
            //command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 100).Value = user.Password;
            //data types may be found in DB

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "SELECT * FROM CATS WHERE USER = @uname AND PASSWORD = @pword)";
                command.Parameters.AddWithValue("@uname", user.UserName);
                command.Parameters.AddWithValue("@pword", user.Password);
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        success = true;
                    }
                }
                connection.Close();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }


        public int AddUser(UserModel newUser)
        {
            byte[] PasswordSalt = new byte[32];
            byte[] PasswordHash = new byte[32];
            int newRows = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();

                command.CommandText = "INSERT INTO CATS (USER, PASSWORD, EMAIL) " +
                    "VALUES (@THISUSER, @THISPASS, @THISEMAIL)";

                using(var hmac = new HMACSHA512())
                {

                    PasswordSalt = hmac.Key;
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(newUser.Password));

                }
                //byte[] PasswordHash
                //byte[] PasswordSalt

                command.Parameters.AddWithValue("@THISUSER", newUser.UserName);
                command.Parameters.AddWithValue("@THISPASS", PasswordHash);
                command.Parameters.AddWithValue("@THISEMAIL", newUser.Email);
                command.Parameters.AddWithValue("@THISEMAIL", PasswordSalt);
                command.Connection = connection;
                newRows = command.ExecuteNonQuery();
                // close connection after query 
                connection.Close();


            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return newRows;
        }

    }
}
