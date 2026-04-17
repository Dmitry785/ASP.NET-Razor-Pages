using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Program.Pages
{
    public class EditMovieModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public void OnGet()
        {
        }
    }
}
