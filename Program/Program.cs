using Application;
using Application.MoviesApi;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Program
{
    public class Program
    {
        //Инициализирует начальные данные при пустом хранилище
        const bool USE_DEFAULT_DATA = true;
        //Если true, использует OMDb API для загрузки фильмов,
        //иначе данные по умолчанию
        const bool USE_DEFAULT_DATA_API = false;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder( new WebApplicationOptions
            {
                WebRootPath = "static"
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddAppDbContext_Sqlite(builder.Configuration);

            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            var app = builder.Build();

            var scopedServiceProvider = app.Services.GetRequiredService<IServiceScopeFactory>();
            var scope = scopedServiceProvider.CreateScope();
            var moviesDataAccess = scope.ServiceProvider.GetRequiredService<IDataAccessService<Movie>>();
            var directorsDataAccess = scope.ServiceProvider.GetRequiredService<IDataAccessService<Director>>();
            var genreDataAccess = scope.ServiceProvider.GetRequiredService<IDataAccessService<Genre>>();
            if (USE_DEFAULT_DATA && moviesDataAccess.GetAll().Data?.Count == 0)
            {
                if (USE_DEFAULT_DATA_API)
                {
                    MoviesApiService moviesApi = new MoviesApiService("5b03492e");
                    foreach (var movieData in moviesApi.LoadSomeMovies().Result)
                    {
                        var directorsResult = directorsDataAccess.GetAll();
                        var genresResult = genreDataAccess.GetAll();
                        if (directorsResult.Success && genresResult.Success)
                        {
                            var director = directorsResult.Data!.FirstOrDefault(x=>x.FullName == movieData.Director);
                            var genre = genresResult.Data!.FirstOrDefault(x=>x.Name == movieData.Genre);
                            if (director == null)
                                director = new Director(movieData.Director);
                            if (genre == null)
                                genre = new Genre(movieData.Genre);
                            var movie = new Movie(movieData.Name, movieData.Description, movieData.Year,
                                director, genre, movieData.Poster);
                            moviesDataAccess.Create(movie);
                        }
                    }
                }
                else
                {
                    var director1 = new Director("Director 1");
                    var director2 = new Director("Director 2");
                    var genre1 = new Genre("Genre 1");
                    var genre2 = new Genre("Genre 2");
                    Movie movie1 = new Movie("Movie 1", "Movie 1 desc", 1999, director1, genre1),
                     movie2 = new Movie("Movie 2", "Movie 2 desc", 1999, director1, genre2),
                     movie3 = new Movie("Movie 3", "Movie 3 desc", 1999, director1, genre2),
                     movie4 = new Movie("Movie 4", "Movie 4 desc", 1999, director2, genre1),
                     movie5 = new Movie("Movie 5", "Movie 5 desc", 1999, director2, genre2);
                    /*Schedule schedule1 = new Schedule(DateTime.Now, movie1),
                        schedule2 = new Schedule(DateTime.Now, movie2),
                        schedule3 = new Schedule(DateTime.Now, movie3),
                        schedule4 = new Schedule(DateTime.Now, movie4),
                        schedule5 = new Schedule(DateTime.Now, movie3),
                        schedule6 = new Schedule(DateTime.Now, movie2),
                        schedule7 = new Schedule(DateTime.Now, movie2);*/
                    movie1.Schedules.Add(new Schedule(DateTime.Now));
                    movie1.Schedules.Add(new Schedule(DateTime.Now));
                    movie2.Schedules.Add(new Schedule(DateTime.Now));
                    movie3.Schedules.Add(new Schedule(DateTime.Now));
                    movie3.Schedules.Add(new Schedule(DateTime.Now));
                    movie3.Schedules.Add(new Schedule(DateTime.Now));
                    movie4.Schedules.Add(new Schedule(DateTime.Now));
                    movie5.Schedules.Add(new Schedule(DateTime.Now));
                    movie5.Schedules.Add(new Schedule(DateTime.Now));
                    moviesDataAccess.AddRange(new List<Movie>()
                    {
                        movie1,
                        movie2,
                        movie3,
                        movie4,
                        movie5
                    });
                }
            }
            app.UseStaticFiles();
            app.MapRazorPages();

            app.Run();
        }
    }
}
