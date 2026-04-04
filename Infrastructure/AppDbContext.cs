using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure
{
    public class AppDbContext : DbContext, IDataStorage
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public AppDbContext(DbContextOptions options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasIndex(x => x.Id);
            modelBuilder.Entity<Director>().HasIndex(x => x.Id);
            modelBuilder.Entity<Genre>().HasIndex(x => x.Id);
        }
    }
}
