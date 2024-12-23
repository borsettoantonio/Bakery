using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages.ProAjax
{
    public class ajax1Model : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public void OnPost()
        {
        }
        public void OnGet()
        {
        }
    }

    public class InputModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
