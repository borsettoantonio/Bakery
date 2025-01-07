using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bakery.Services.Application;

namespace Bakery.Pages.ProAjax
{
    public class ajaxPartialModel : PageModel
    {
        private ICarService _carService;
        public ajaxPartialModel(ICarService carService)
        {
            _carService = carService;
        }
        public List<Car>? Cars { get; set; }
        public void OnGet()
        {
        }
        public PartialViewResult OnGetCarPartial()
        {
            Cars = _carService.GetAll();
            return Partial("_CarPartial", Cars);
        }
    }
}
