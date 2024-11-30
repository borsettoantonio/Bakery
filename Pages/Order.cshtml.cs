using System.ComponentModel.DataAnnotations;
using Bakery.Models;
using Bakery.Services.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Bakery.Pages;
public class OrderModel : PageModel
{
    private readonly IProdotti prod;
    public OrderModel(IProdotti _prod) =>
        this.prod = _prod!;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    public Product? Product { get; set; }
    [BindProperty, Range(1, int.MaxValue, ErrorMessage = "You must order at least one item")]
    public int Quantity { get; set; } = 1;

    [BindProperty, Required, EmailAddress, Display(Name = "Your Email Address")]
    public string OrderEmail { get; set; }

    [BindProperty, Required, Display(Name = "Shipping Address")]
    public string ShippingAddress { get; set; }

    [BindProperty]
    public decimal UnitPrice { get; set; }

    [TempData]
    public string Confirmation { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Product = await prod.FindAsync(Id);
        if (Product is null)
        {
            return RedirectToPage("Error");
        }
        else
        {
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Product = await prod.FindAsync(Id);
        if (ModelState.IsValid)
        {
            Confirmation = @$"You have ordered {Quantity} x {Product.Name} 
                        at a total cost of {Quantity * UnitPrice:c}";
            return RedirectToPage("/OrderSuccess");
        }
        return Page();

    }
}