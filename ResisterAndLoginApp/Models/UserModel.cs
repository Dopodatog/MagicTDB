using System.Text;
using System.Security.Cryptography;
namespace MagicTDB.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }



        public string toString(){
            return "Name: " + UserName + " Password: " + Password; 
        }
    }
}
