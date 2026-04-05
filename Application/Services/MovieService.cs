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
            return Result.Ok();
        }
        public Result<List<Movie>> GetAll()
        {
            return Result.Ok(_dataStorage.Movies.Include(x=>x.Director).Include(x=>x.Schedules).AsNoTracking().ToList());
        }
        public Result<Movie> GetById(Guid id)
        {
            var movie = _dataStorage.Movies.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (movie is null)
                return Result<Movie>.Fail("Не удалось найти фильм");
            return Result.Ok(movie);
        }

        public Result Update(Movie movie)
        {
            var updateMovie = _dataStorage.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (updateMovie is null)
                return Result<Movie>.Fail("Не удалось найти фильм");
            updateMovie = movie;
            return Result.Ok();
        }
    }
}
