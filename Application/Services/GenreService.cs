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
    public class GenreService : IDataAccessService<Genre>
    {
        private readonly IDataStorage _dataStorage;
        public GenreService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public Result AddRange(List<Genre> genres)
        {
            _dataStorage.Genres.AddRange(genres);
            return Result.Ok();
        }
        public Result<Guid> Create(Genre genre)
        {
            var id = _dataStorage.Genres.Add(genre).Entity.Id;
            _dataStorage.SaveChanges();
            return Result.Ok(id);
        }
        public Result DeleteById(Guid id)
        {
            var genre = _dataStorage.Genres.FirstOrDefault(x => x.Id == id);
            if (genre is null)
                return Result.Fail("Не удалось найти фильм");
            _dataStorage.Genres.Remove(genre);
            return Result.Ok();
        }
        public Result<List<Genre>> GetAll()
        {
            return Result.Ok(_dataStorage.Genres.AsNoTracking().ToList());
        }
        public Result<Genre> GetById(Guid id)
        {
            var genre = _dataStorage.Genres.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (genre is null)
                return Result<Genre>.Fail("Не удалось найти фильм");
            return Result.Ok(genre);
        }

        public Result Update(Genre genre)
        {
            var updateGenre = _dataStorage.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (updateGenre is null)
                return Result<Genre>.Fail("Не удалось найти фильм");
            updateGenre = genre;
            return Result.Ok();
        }
    }
}
