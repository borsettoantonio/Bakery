using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Select.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public Stato stato { get; set; } = new();  // contiene lo stato da preservare
    [BindProperty(SupportsGet = true)]
    public string statoStr { get; set; }        // contiene la stringa in Base64 della rappresentazione json dello stato
    [BindProperty]
    public int count { get; set; }              // campo di input della pagina


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        //stato.cnt = 0;
        if (statoStr != null)
        {
            stato = DecodeFromBase64(statoStr);
            count = stato.cnt;
        }
        else
        {
            statoStr = EncodeToBase64(stato);
        }

        ViewData["stato"] = statoStr;
    }

    public void OnPost()
    {
        stato = DecodeFromBase64(statoStr);
        ++stato.cnt;
        count = stato.cnt;
        statoStr = EncodeToBase64(stato);
        ViewData["stato"] = statoStr;
    }

    private string EncodeToBase64(Stato value)
    {
        var valueBytes = JsonSerializer.SerializeToUtf8Bytes<Stato>(value);
        return Convert.ToBase64String(valueBytes);
    }
    private Stato DecodeFromBase64(string value)
    {
        var valueBytes = System.Convert.FromBase64String(value);
        var readOnlySpan = new ReadOnlySpan<byte>(valueBytes);
        return JsonSerializer.Deserialize<Stato>(readOnlySpan)!;
    }

    // per poter trasformare in json la classe occorre 
    // usare le proprietà e non i campi ( che non vengono scritti in json)
    public class Stato
    {
        public int cnt { get; set; } = 0;
        public string nome { get; set; } = "Antonio";
    }
}

