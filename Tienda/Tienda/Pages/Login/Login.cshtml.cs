using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class LoginModel : PageModel
    {
        private readonly IIdentityRepository _repositoryIdentity;

        public LoginModel(IIdentityRepository repositoryIdentity)
        {
            _repositoryIdentity = repositoryIdentity;
        }

        public IActionResult OnPost()
        {
            var customer = new Customer()
            {
                Username = Request.Form["username"],
                Password = Request.Form["password"]
            };

            var customerReceived = _repositoryIdentity.UserExists(customer);
            if (customerReceived != null)
            {
                HttpContext.Session.SetString("UserSession", customerReceived.Id.ToString());
                return RedirectToPage("../Market/Market");
            }
            return RedirectToPage("../Error");
        }
    }
}