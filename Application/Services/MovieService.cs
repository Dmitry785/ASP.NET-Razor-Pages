using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MovieService : IDataAccessService<Movie>
    {
        private readonly IDataStorage _dataStorage;
        public MovieService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public Result AddRange(List<Movie> movies)
        {
            _dataStorage.Movies.AddRange(movies);
            _dataStorage.SaveChanges();
            return Result.Ok();
        }
        public Result<Guid> Create(Movie movie)
        {
            var id = _dataStorage.Movies.Add(movie).Entity.Id;
            _dataStorage.SaveChanges();
            return Result.Ok(id);
        }
        public Result DeleteById(Guid id)
        {
            var movie = _dataStorage.Movies.FirstOrDefault(x => x.Id == id);
            if (movie is null)
                return Result.Fail("Не удалось найти фильм");
            _dataStorage.Movies.Remove(movie);
            _dataStorage.SaveChanges();
            return Result.Ok();
        }
        public Result<List<Movie>> GetAll()
        {
            return Result.Ok(_dataStorage.Movies.Include(x=>x.Director).Include(x=>x.Schedules).Include(x=>x.Genre).AsNoTracking().ToList());
        }
        public Result<Movie> GetById(Guid id)
        {
            var movie = _dataStorage.Movies.Include(x=>x.Director).Include(x=>x.Schedules)
                .Include(x=>x.Genre).AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (movie is null)
                return Result<Movie>.Fail("Не удалось найти фильм");
            return Result.Ok(movie);
        }

        public Result Update(Movie movie)
        {
            // 1. Загружаем существующий фильм со всеми связями
            var existingMovie = _dataStorage.Movies
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .FirstOrDefault(m => m.Id == movie.Id);

            if (existingMovie == null)
                return Result.Fail("Не удалось найти фильм");

            _dataStorage.Entry(existingMovie).CurrentValues.SetValues(movie);

            if (movie.Director != null)
            {
                var dbDirector = _dataStorage.Directors.Find(movie.Director.Id);
                if (dbDirector == null)
                {
                    existingMovie.Director = movie.Director;
                    _dataStorage.Entry(movie.Director).State = EntityState.Added;
                }
                else
                {
                    existingMovie.Director = dbDirector;
                }
            }
            if (movie.Genre != null)
            {
                var dbGenre = _dataStorage.Genres.Find(movie.Genre.Id);
                if (dbGenre == null)
                {
                    existingMovie.Genre = movie.Genre;
                    _dataStorage.Entry(movie.Genre).State = EntityState.Added;
                }
                else
                {
                    existingMovie.Genre = dbGenre;
                }
            }
            _dataStorage.SaveChanges();
            return Result.Ok();
        }
    }
}
