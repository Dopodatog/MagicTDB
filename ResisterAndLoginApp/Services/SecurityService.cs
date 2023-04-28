using ResisterAndLoginApp.Models;

namespace ResisterAndLoginApp.Services
{
    public class SecurityService
    {
        UsersDAO usersDAO = new UsersDAO();
        public SecurityService()
        {

        }
        public bool IsValid(UserModel user)
        {
            return usersDAO.FindUserByNameAndPassword(user);

        }
    }
}
