using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public interface IDataStorage
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        Task<int> SaveChangesAsync(CancellationToken token);
        int SaveChanges();
    }
}
