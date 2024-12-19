using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bakery.Models;
using System.ComponentModel.DataAnnotations;
using Bakery.Services.Application;
using System.Globalization;
using System.ComponentModel;

namespace Bakery.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProdotti prod;
        private readonly IWebHostEnvironment environment;

        public CreateModel(IProdotti _prod, IWebHostEnvironment environment)
        {
            prod = _prod;
            this.environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public string dec_string { get; set; }
        [BindProperty, Display(Name = "Product Image")]
        public IFormFile ProductImage { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /* var separatore = CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
            var separatoreInv = CultureInfo.InvariantCulture.NumberFormat.CurrencyGroupSeparator; */

            bool errore = dec_string.Contains('.') ? true : false;
            Product.Price = decimal.Parse(dec_string, CultureInfo.CurrentCulture);

            ModelState.Remove("Product.ImageName");
            if (!ModelState.IsValid || prod == null || Product == null) // || errore)
            {
                return Page();
            }

            Product.ImageName = ProductImage.FileName;
            var imageFile = Path.Combine(environment.WebRootPath, "images", "products", ProductImage.FileName);
            using var fileStream = new FileStream(imageFile, FileMode.Create);
            await ProductImage.CopyToAsync(fileStream);
            await prod.Add(Product);

            return RedirectToPage("./Index");
        }
    }
}
