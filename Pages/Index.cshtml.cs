using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Select.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public int[]? Number { get; set; }
    private readonly ILogger<IndexModel> _logger;
    //public SelectList Options { get; set; }
    public List<SelectListItem> Options { get; set; }
    Dictionary<int, string> dictionary = new() { { 1, "uno" }, { 2, "due" } };
    Persona[] persone = new[]{
        new Persona{Id=1 ,Nome="pippo",Sede="Roma"},
        new Persona{Id=2 ,Nome="pippo1",Sede="Rovigo"},
        new Persona{Id=3 ,Nome="pippo2",Sede="Roma1"},
        new Persona{Id=4 ,Nome="pippo3",Sede="Rovigo1"}
    };

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        //Options = new SelectList(dictionary, "Key", "Value");
        //Options = new SelectList(persone, nameof(Persona.Id), nameof(Persona.Nome));
        SelectListGroup g1 = new SelectListGroup() { Name = "G1" };
        SelectListGroup g2 = new SelectListGroup() { Name = "G2" };
        Options = (from p in persone
                   select (new SelectListItem
                   {
                       Group = (p.Id > 2) ? g1 : g2,
                       Value = p.Id.ToString(),
                       Text = p.Sede
                   })).ToList();
    }

    public void OnGet()
    {
        var x = dictionary.Keys;
    }

    public void OnPost()
    {

    }
}

public class Persona
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sede { get; set; }
}
