using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class CreateModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;
        private IHostingEnvironment _IhostingEnvironment;

        public CreateModelProduct(IRepository<Product> repository, IHostingEnvironment IhostingEnvironment)
        {
            _repository = repository;
            _IhostingEnvironment = IhostingEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Image = await GetBytes(Upload);

            _repository.Create(Product);

            return RedirectToPage("./Index");
        }

        public static async Task<byte[]> GetBytes(IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
