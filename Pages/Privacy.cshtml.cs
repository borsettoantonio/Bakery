using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Select.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    // [TempData]
    // public string statoStr { get; set; }        // contiene la stringa in Base64 della rappresentazione json dello stato

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        //TempData["statoStr"] = statoStr;
    }

}

