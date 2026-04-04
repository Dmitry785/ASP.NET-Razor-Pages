using Application;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Program
{
    public class Program
    {
        const bool USE_DEFAULT_DATA = true;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddAppDbContext_Sqlite(builder.Configuration);

            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            var app = builder.Build();

            var scopedServiceProvider = app.Services.GetRequiredService<IServiceScopeFactory>();
            var scope = scopedServiceProvider.CreateScope();
            var moviesDataAccess = scope.ServiceProvider.GetRequiredService<IDataAccessService<Movie>>();
            if (USE_DEFAULT_DATA && moviesDataAccess.GetAll().Data?.Count == 0)
            {
                var director1 = new Director("Director 1 fname", "Director 1 lname");
                var director2 = new Director("Director 2 fname", "Director 2 lname");
                var genre1 = new Genre("Genre 1");
                var genre2 = new Genre("Genre 2");
                Movie movie1 = new Movie("Movie 1", "Movie 1 desc", director1, genre1),
                 movie2 = new Movie("Movie 2", "Movie 2 desc", director1, genre2),
                 movie3 = new Movie("Movie 3", "Movie 3 desc", director1, genre2),
                 movie4 = new Movie("Movie 4", "Movie 4 desc", director2, genre1),
                 movie5 = new Movie("Movie 5", "Movie 5 desc", director2, genre2);
                Schedule schedule1 = new Schedule(DateTime.Now, movie1),
                    schedule2 = new Schedule(DateTime.Now, movie2),
                    schedule3 = new Schedule(DateTime.Now, movie3),
                    schedule4 = new Schedule(DateTime.Now, movie4),
                    schedule5 = new Schedule(DateTime.Now, movie3),
                    schedule6 = new Schedule(DateTime.Now, movie2),
                    schedule7 = new Schedule(DateTime.Now, movie2);
                movie1.Schedules.Add(schedule1);
                moviesDataAccess.Create();
            }

            app.MapRazorPages();

            app.Run();
        }
    }
}
