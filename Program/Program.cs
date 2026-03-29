
namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            var app = builder.Build();

            app.MapRazorPages();

            app.Run();
        }
    }
}
