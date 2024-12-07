using System.Text.Json;
using Bakery.Models;
using Bakery.Services.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MimeKit;
using MailKit.Net.Smtp;


namespace Bakery.Pages;
public class CheckoutModel : PageModel
{
    private readonly IProdotti prod;
    private readonly IWebHostEnvironment environment;
    public CheckoutModel(IProdotti _prod, IWebHostEnvironment _environment)
    {
        prod = _prod;
        environment = _environment;
    }
    [BindProperty, Required, Display(Name = "Your Email Address")]
    public string OrderEmail { get; set; }
    [BindProperty, Required, Display(Name = "Shipping Address")]
    public string ShippingAddress { get; set; }
    [TempData]
    public string Confirmation { get; set; }
    public Basket Basket { get; set; } = new();
    public List<Product> SelectedProducts { get; set; } = new();
    public async Task OnGetAsync()
    {
        if (Request.Cookies[nameof(Basket)] is not null)
        {
            Basket = JsonSerializer.Deserialize<Basket>(Request.Cookies[nameof(Basket)]);
            if (Basket.NumberOfItems > 0)
            {
                var selectedProducts = Basket.Items.Select(b => b.ProductId).ToArray();
                var lista = await prod.GetProdotti();
                SelectedProducts = lista.Select(x => x).Where(p => selectedProducts.Contains(p.Id)).ToList();
            }
        }
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid && Request.Cookies[nameof(Basket)] is not null)
        {
            var basket = JsonSerializer.Deserialize<Basket>(Request.Cookies[nameof(Basket)]);
            if (basket is not null)
            {
                var plural = basket.NumberOfItems == 1 ? string.Empty : "s";
                Confirmation = $@"<p>Your order for {basket.NumberOfItems} item{plural} has been received and is being processed:</p>
            <p>It will be sent to {ShippingAddress}. We will notify you when it has been despatched</p>";

                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse("test@test.com"));
                message.To.Add(MailboxAddress.Parse(OrderEmail));
                message.Subject = "Your order confirmation";
                message.Body = new TextPart("html")
                {
                    Text = Confirmation
                };

                BodyBuilder _body = new BodyBuilder
                {
                    HtmlBody = Confirmation
                };
                var _fileName = Path.Combine(environment.WebRootPath, "images", "products", "bagels.jpg");
                // anche questo funziona
                //string _fileName = "wwwroot\\images\\products\\bagels.jpg";
                FileStream str = System.IO.File.Open(_fileName, FileMode.Open);
                _body.Attachments.Add(_fileName, str);
                message.Body = _body.ToMessageBody();


                // per usare Google
                using SmtpClient client = new();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("borsettoantonio@gmail.com", "oygakygwwcvlsqkw");


                /* // per usare smtp4dev
                using var client = new SmtpClient();
                await client.ConnectAsync("localhost"); */

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                Response.Cookies.Append(nameof(Basket), string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                return RedirectToPage("/OrderSuccess");
            }
        }
        return Page();
    }
}