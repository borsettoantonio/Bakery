using Microsoft.AspNetCore.Mvc;

namespace Bakery.Pages.Components.prova3;

public class provaTreCompViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int i)
    {
        return View(i);
    }
}
