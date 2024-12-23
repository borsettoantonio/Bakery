using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages.Prove
{
    public class provaformModel : PageModel
    {
        public string testo { get; set; }
        [BindProperty]
        public string nome { get; set; }
        [BindProperty]
        public int numero { get; set; }
        [BindProperty]
        public string btnuno { get; set; }
        [BindProperty]
        public string btndue { get; set; }


        public void OnPost()
        {
            if (btnuno == "uno")
                testo = "uno";
            else
                testo = "due";
        }

    }
}