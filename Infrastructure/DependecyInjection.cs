using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Program
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddAppDbContext_SqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Movies");
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped<IDataStorage>(provider => provider.GetRequiredService<AppDbContext>());
            return services;
        }
        public static IServiceCollection AddAppDbContext_Sqlite(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Movies_Sqlite");
            services.AddDbContext<AppDbContext>(x => x.UseSqlite(connectionString));
            services.AddScoped<IDataStorage>(provider => provider.GetRequiredService<AppDbContext>());
            return services;
        }
    }
}
