using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResisterAndLoginApp.Controllers
{
    internal class MyAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        //the IAuthorizationFilter is in using Microsoft.AspNetCore.Mvc.Filters
        //and is standard for C# .net core
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userName = context.HttpContext.Session.GetString("username");
            if (userName == null)
            {
                context.Result = new RedirectResult("/login");
            }
        }
    }
}