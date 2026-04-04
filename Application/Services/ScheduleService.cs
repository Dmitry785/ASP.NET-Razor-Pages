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
    public class ScheduleService : IDataAccessService<Schedule>
    {
        private readonly IDataStorage _dataStorage;
        public ScheduleService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        public Result<Guid> Create(Schedule schedule)
        {
            var id = _dataStorage.Schedules.Add(schedule).Entity.Id;
            _dataStorage.SaveChanges();
            return Result.Ok(id);
        }
        public Result DeleteById(Guid id)
        {
            var schedule = _dataStorage.Schedules.FirstOrDefault(x => x.Id == id);
            if (schedule is null)
                return Result.Fail("Не удалось найти фильм");
            _dataStorage.Schedules.Remove(schedule);
            return Result.Ok();
        }
        public Result<List<Schedule>> GetAll()
        {
            return Result.Ok(_dataStorage.Schedules.AsNoTracking().ToList());
        }
        public Result<Schedule> GetById(Guid id)
        {
            var schedule = _dataStorage.Schedules.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (schedule is null)
                return Result<Schedule>.Fail("Не удалось найти фильм");
            return Result.Ok(schedule);
        }

        public Result Update(Schedule schedule)
        {
            var updateSchedule = _dataStorage.Schedules.FirstOrDefault(x => x.Id == schedule.Id);
            if (updateSchedule is null)
                return Result<Movie>.Fail("Не удалось найти фильм");
            updateSchedule = schedule;
            return Result.Ok();
        }
    }
}
