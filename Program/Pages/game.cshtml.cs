using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Program.Pages
{
    public class gameModel : PageModel
    {
        public void OnGet(Guid id)
        {
            Console.WriteLine("Hello");
        }
    }
}
