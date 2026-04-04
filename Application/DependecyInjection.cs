using Application.Services;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Program
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IDataAccessService<Movie>, MovieService>();
            services.AddTransient<IDataAccessService<Schedule>, ScheduleService>();
            return services;
        }
    }
}
