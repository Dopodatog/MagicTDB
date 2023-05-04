using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace MagicTDB.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string toString(){
            return "Name: " + UserName + " Password: " + Password; 
        }
    }
}
