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
    public class DirectorService : IDataAccessService<Director>
    {
        private readonly IDataStorage _dataStorage;
        public DirectorService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public Result AddRange(List<Director> directors)
        {
            _dataStorage.Directors.AddRange(directors);
            return Result.Ok();
        }
        public Result<Guid> Create(Director director)
        {
            var id = _dataStorage.Directors.Add(director).Entity.Id;
            _dataStorage.SaveChanges();
            return Result.Ok(id);
        }
        public Result DeleteById(Guid id)
        {
            var director = _dataStorage.Directors.FirstOrDefault(x => x.Id == id);
            if (director is null)
                return Result.Fail("Не удалось найти фильм");
            _dataStorage.Directors.Remove(director);
            return Result.Ok();
        }
        public Result<List<Director>> GetAll()
        {
            return Result.Ok(_dataStorage.Directors.AsNoTracking().ToList());
        }
        public Result<Director> GetById(Guid id)
        {
            var director = _dataStorage.Directors.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (director is null)
                return Result<Director>.Fail("Не удалось найти фильм");
            return Result.Ok(director);
        }

        public Result Update(Director director)
        {
            var updateDirector = _dataStorage.Directors.FirstOrDefault(x => x.Id == director.Id);
            if (updateDirector is null)
                return Result<Director>.Fail("Не удалось найти фильм");
            updateDirector = director;
            return Result.Ok();
        }
    }
}
