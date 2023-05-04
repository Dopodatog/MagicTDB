using MagicTDB.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MagicTDB.Services
{
    public class UsersDAO
    {
        static string saltFather = "TooSalty";

        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=users;";


        public string HashString(string inputString)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(saltFather);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);

            byte[] saltedInputBytes = new byte[saltBytes.Length + inputBytes.Length];
            saltBytes.CopyTo(saltedInputBytes, 0);
            inputBytes.CopyTo(saltedInputBytes, saltBytes.Length);

            SHA256 sha = SHA256.Create();
            byte[] hashBytes = sha.ComputeHash(saltedInputBytes);

            return Convert.ToBase64String(hashBytes);
        }
        public bool FindUserByNameAndPassword(UserModel user)
        {

            bool success = false;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                string PasswordHash = HashString(user.Password);

                command.CommandText = "SELECT * FROM CATS WHERE CAT_USER = @uname AND CAT_PASSWORD = @pword";
                command.Parameters.AddWithValue("@uname", user.UserName);
                command.Parameters.AddWithValue("@pword", PasswordHash);
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

            int newRows = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();

                command.CommandText = "INSERT INTO CATS (CAT_USER, CAT_PASSWORD, CAT_EMAIL) " +
                    "VALUES (@THISUSER, @THISPASS, @THISEMAIL)";


                string PasswordHash = HashString(newUser.Password);
                

                command.Parameters.AddWithValue("@THISUSER", newUser.UserName);
                command.Parameters.AddWithValue("@THISPASS", PasswordHash);
                command.Parameters.AddWithValue("@THISEMAIL", newUser.Email);
                command.Connection = connection;
                newRows = command.ExecuteNonQuery();
        
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
