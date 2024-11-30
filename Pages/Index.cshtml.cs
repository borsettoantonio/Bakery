using Bakery.Models;
using Bakery.Services.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages;
public class IndexModel : PageModel
{
    private readonly IProdotti prod;
    public IndexModel(IProdotti _prod) =>
        this.prod = _prod;
    public List<Product> Products { get; set; } = new();
    public async Task OnGet()
    {
        Products = await prod.GetProdotti();
    }
}