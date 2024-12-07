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
    public class IndexModel : PageModel
    {
        private readonly IProdotti prod;

        public IndexModel(IProdotti _prod)
        {
            prod = _prod;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await prod.GetProdotti();
        }
    }
}
