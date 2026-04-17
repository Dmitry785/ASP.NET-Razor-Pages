using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Program.Pages
{
    public class MovieModel : PageModel
    {
        public IDataAccessService<Movie> Movies { get; }
        public IDataAccessService<Director> Directors { get; }
        public IDataAccessService<Genre> Genres { get; }
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public MovieModel(IDataAccessService<Movie> movies, 
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
        public IActionResult OnPut(string name, string director, string genre,
            string? poster, string year, string desc)
        {
            if (!int.TryParse(year, out int yearInt))
                return NotFound();
            var directorsAll = Directors.GetAll();
            var genresAll = Genres.GetAll();
            Director currentDirector = directorsAll.Data?.FirstOrDefault(x => x.FullName == director) ?? new Director(director);
            Genre currentGenre = genresAll.Data?.FirstOrDefault(x => x.Name == genre) ?? new Genre(genre);

            Console.WriteLine(currentGenre.Name);
            Console.WriteLine(currentDirector.FullName);
            if (Movies.Update(new Movie(name, desc, yearInt, currentDirector, currentGenre, poster)
                {Id = this.Id}).Success)
                return new OkResult();
            return BadRequest();
        }
        public IActionResult OnDelete()
        {
            if (Movies.DeleteById(Id).Success)
                return new OkResult();
            return new NotFoundResult();
        }
    }
}
