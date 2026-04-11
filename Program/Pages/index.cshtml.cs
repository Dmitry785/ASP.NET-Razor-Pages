using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Services.Interfaces;

namespace Program.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "name")]
        public string? FilterName { get; set; }
        [BindProperty(SupportsGet = true, Name = "desc")]
        public string? FilterDescription { get; set; }
        [BindProperty(SupportsGet = true, Name = "director")]
        public string? FilterDirector { get; set; }
        [BindProperty(SupportsGet = true, Name = "genre")]
        public string? FilterGenre { get; set; }
        [BindProperty(SupportsGet = true, Name = "session_time")]
        public DateOnly? FilterSessionTime { get; set; }
        public IndexModel()
        {
        }
        public void OnGet()
        {

        }
    }
}
