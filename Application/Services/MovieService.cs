using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IDataStorage _dataStorage;
        public MovieService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        public Result<Guid> Create(Movie movie)
        {
            var id = _dataStorage.Movies.Add(movie).Entity.Id;
            _dataStorage.SaveChanges();
            return Result.Ok(id);
        }
        public Result DeleteById(Guid id)
        {

        }

        public Result<List<Movie>> GetAllMovies()
        {
            throw new NotImplementedException();
        }

        public Result<Movie> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
