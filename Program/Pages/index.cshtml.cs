using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Services.Interfaces;

namespace Program.Pages
{
    public class IndexModel : PageModel
    {
        public IDataAccessService<Movie> MovieService { get; set; }
        public IndexModel(IDataAccessService<Movie> service)
        {
            MovieService = service;
        }
        public void OnGet()
        {
            var movies = MovieService.GetAll();
            if (movies.Failed)
                return;
            ViewData.Add("Movies", movies.Data!);
        }
    }
}
