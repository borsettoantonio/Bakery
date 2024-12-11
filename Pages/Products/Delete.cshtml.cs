using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using Bakery.Models;
using Bakery.Services.Application;

namespace Bakery.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProdotti prod;
        [BindProperty]
        public Product Product { get; set; } = default!;

        public DeleteModel(IProdotti _prod)
        {
            prod = _prod;
        }

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
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || prod == null)
            {
                return NotFound();
            }
            await prod.Delete((int)id);

            return RedirectToPage("./Index");
        }
    }
}
