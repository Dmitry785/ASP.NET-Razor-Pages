using Application;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddAppDbContext_Sqlite(builder.Configuration);

            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            var app = builder.Build();

            app.MapRazorPages();

            app.Run();
        }
    }
}
