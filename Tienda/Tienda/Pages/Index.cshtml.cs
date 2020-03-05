using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tienda.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            return RedirectToPage("./Market/Market");
        }
    }
}
