using Bakery.Services.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages
{

    public class InitDbModel : PageModel
    {
        private readonly IInitDb inizializzaDb;
        public InitDbModel(IInitDb _inizializzaDb)
        {
            inizializzaDb = _inizializzaDb;
        }
        public async void OnGet()
        {
            await inizializzaDb.InitDatabase();
        }


    }
}
