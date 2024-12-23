using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages.Prove
{
    public class check1Model : PageModel
    {
        [BindProperty, Display(Name = "Promosso")]
        public bool IsChecked { get; set; }
        [BindProperty, Display(Name = "Italiano")]
        public bool IsChecked2 { get; set; }
        [BindProperty, Display(Name = "Russo")]
        public bool IsChecked3 { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (IsChecked)
                Console.WriteLine("true");
        }
    }
}
