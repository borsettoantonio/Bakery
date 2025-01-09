using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bakery.Services.Application;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bakery.Pages.ProAjax;

public class CascadingDropdownsModel : PageModel
{
    private ICategoryService categoryService;
    public CascadingDropdownsModel(ICategoryService categoryService) => this.categoryService = categoryService;
    [BindProperty(SupportsGet = true)]
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public SelectList? Categories { get; set; }

    public void OnGet()
    {
        Categories = new SelectList(categoryService.GetCategories(), nameof(Category.CategoryId), nameof(Category.CategoryName));
    }
    public JsonResult OnGetSubCategories()
    {
        return new JsonResult(categoryService.GetSubCategories(CategoryId));
    }
}