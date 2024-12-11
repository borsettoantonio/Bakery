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
    public class DetailsModel : PageModel
    {
        private readonly IProdotti prod;

        public DetailsModel(IProdotti _prod)
        {
            prod = _prod;
        }

        public Product Product { get; set; } = default!;

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

    }
}
