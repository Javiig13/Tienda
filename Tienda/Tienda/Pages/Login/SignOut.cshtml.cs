using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Tienda.DataAccess;

namespace Tienda
{
    public class SignOutModel : PageModel
    {
        private readonly DatabaseContext _dbContext;

        public SignOutModel(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            var items = _dbContext.CartItems.ToList();
            _dbContext.CartItems.RemoveRange(items);
            HttpContext.Session.Remove("UserSession");
            return RedirectToPage("./Login/Login");
        }
    }
}