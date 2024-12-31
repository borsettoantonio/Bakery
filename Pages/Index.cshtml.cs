using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Select.Services;


namespace Select.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public Stato stato { get; set; } = new();  // contiene lo stato da preservare
    // [TempData]
    // public string statoStr { get; set; }        // contiene la stringa in Base64 della rappresentazione json dello stato
    [BindProperty]
    public int count { get; set; }              // campo di input della pagina


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var statoStr = (string)TempData["statoStr"]!;
        if (statoStr != null)
        {
            stato = DecodeFromBase64<Stato>(statoStr);
            count = stato.cnt;
        }
        TempData["statoStr"] = EncodeToBase64(stato);
        //statoStr = EncodeToBase64(stato);
    }

    public void OnPost()
    {
        var statoStr = (string)TempData["statoStr"]!;
        if (statoStr != null)
        {
            stato = DecodeFromBase64<Stato>(statoStr);
            ++stato.cnt;
            count = stato.cnt;
            TempData["statoStr"] = EncodeToBase64(stato);
            //statoStr = EncodeToBase64(stato);
        }
    }
    private string EncodeToBase64<T>(T value)
    {
        var valueBytes = JsonSerializer.SerializeToUtf8Bytes<T>(value);
        return Convert.ToBase64String(valueBytes);
    }
    private T DecodeFromBase64<T>(string value)
    {
        var valueBytes = System.Convert.FromBase64String(value);
        var readOnlySpan = new ReadOnlySpan<byte>(valueBytes);
        return JsonSerializer.Deserialize<T>(readOnlySpan)!;
    }

    // per poter trasformare in json la classe occorre 
    // usare le proprietà e non i campi ( che non vengono scritti in json)
    public class Stato
    {
        public int cnt { get; set; } = 0;
        public string nome { get; set; } = "Antonio";
    }
}

