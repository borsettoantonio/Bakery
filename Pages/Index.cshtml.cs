using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Select.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    //public SelectList Options { get; set; }
    [BindProperty]
    public DateTime DateTime { get; set; } = DateTime.Now;  // new DateTime(2024, 12, 24);

    [BindProperty, DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [BindProperty, DataType(DataType.Time)]
    public DateTime Time { get; set; }

    [BindProperty]
    public string Gender { get; set; }
    public string[] Genders = new[] { "Male", "Female", "Unspecified" };

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        var dt = DateTime.Now;
        var isoDateString = dt.ToString("O");
        Console.WriteLine(isoDateString);

        DateTime dt1 = Date.Add(Time.TimeOfDay);
    }
}

