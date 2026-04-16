using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Program.Pages
{
    public class MovieModel : PageModel
    {
        public IDataAccessService<Movie> Movies { get; }
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public MovieModel(IDataAccessService<Movie> movies)
        {
            Movies = movies;
        }
        public void OnGet()
        {

        }
        public IActionResult OnDelete()
        {
            if (Movies.DeleteById(Id).Success)
                return new OkResult();
            return new NotFoundResult();
        }
    }
}
