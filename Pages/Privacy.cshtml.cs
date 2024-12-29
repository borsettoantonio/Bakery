using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Select.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    [BindProperty(SupportsGet = true)]
    public string statoStr { get; set; }

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        if (statoStr != null)               // prendo lo stato rocevuto e lo inserisco in ViewData
        {                                   // per reinserirlo nella nuova pagina 
            ViewData["stato"] = statoStr;
        }
        else
        {
            ViewData["stato"] = "";
        }
    }
}

