using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ResisterAndLoginApp.Models;
using ResisterAndLoginApp.Services;
using ResisterAndLoginApp.Utility;

namespace ResisterAndLoginApp.Controllers
{
    public class LoginController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //this is a get request
        [MyAuthorization]
        //[Authorize] would be the standard if I wasn't making my own log in 

        public IActionResult PrivateSectionMustBeLoggedIn()
        {
            Console.WriteLine("inside");
            return Content("I am a protected method");     
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {
            MyLogger.GetInstance().Info("Processing a login attempt");
            MyLogger.GetInstance().Info(userModel.toString());

            SecurityService sercurityService = new SecurityService();

            if(sercurityService.IsValid(userModel))
            {
                HttpContext.Session.SetString("username", userModel.UserName);

                MyLogger.GetInstance().Info("Login success");
                return View("LoginSuccess", userModel);
            }
            else
            {
                HttpContext.Session.Remove("username");

                MyLogger.GetInstance().Warning("Login failure");
                return View("LoginFailure", userModel);
            }
            //return View("Logged in!", userModel);
        }
        public IActionResult UserInputForm()
        {
            return View();
        }

        public IActionResult MakeUser(UserModel newUser)
        {
            UsersDAO allUsers = new UsersDAO();
            allUsers.AddUser(newUser);

            return View("Index");
        }
    }
}
