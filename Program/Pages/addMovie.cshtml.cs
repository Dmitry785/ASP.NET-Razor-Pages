using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Program.Pages
{
    public class AddMovieModel : PageModel
    {
        public IDataAccessService<Movie> Movies { get; }
        public IDataAccessService<Director> Directors { get; }
        public IDataAccessService<Genre> Genres { get; }
        public AddMovieModel(IDataAccessService<Movie> movies,
            IDataAccessService<Director> directors,
            IDataAccessService<Genre> genres)
        {
            Movies = movies;
            Directors = directors;
            Genres = genres;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string name, string director, string genre,
            string? poster, string year, string desc)
        {
            if (!int.TryParse(year, out int yearInt))
                return NotFound();
            var directorsAll = Directors.GetAll();
            var genresAll = Genres.GetAll();
            Director currentDirector = directorsAll.Data?.FirstOrDefault(x => x.FullName == director) ?? new Director(director);
            Genre currentGenre = genresAll.Data?.FirstOrDefault(x => x.Name == genre) ?? new Genre(genre);

            if (Movies.Create(new Movie(name, desc, yearInt, currentDirector, currentGenre, poster)).Success)
                return new OkResult();
            return BadRequest();
        }
    }
}
