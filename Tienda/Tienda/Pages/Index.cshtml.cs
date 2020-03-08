using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tienda.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            bool isUserLogged = HttpContext.Session.GetString("UserSession") != null;
            if (isUserLogged)
            {
                return RedirectToPage("./Market/Market");
            }
            else
            {
                return RedirectToPage("./Login/Login");
            }
        }
    }
}
