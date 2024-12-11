using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Bakery.Models;
using Bakery.Services.Application;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProdotti prod;
        public EditModel(IProdotti _prod)
        {
            prod = _prod;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty, Display(Name = "Price")]
        public string dec_string { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || prod == null)
            {
                return NotFound();
            }

            var product = await prod.FindAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            dec_string = Product.Price.ToString();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int num = 0;
            Product.Price = decimal.Parse(dec_string, CultureInfo.CurrentCulture);
            try
            {
                num = await prod.Update(Product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (num == 0)
            {
                Console.WriteLine("Errore su Delete");
            }
            return RedirectToPage("./Index");
        }
    }
}
