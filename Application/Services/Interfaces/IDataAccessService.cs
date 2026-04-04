using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Models;
using Domain.Models.Interfaces;

namespace Application.Services.Interfaces
{
    public interface IDataAccessService<T> where T : BaseModel<Guid>
    {
        Result<List<T>> GetAll();
        Result<T> GetById(Guid id);
        Result<Guid> Create(T movie);
        Result Update(T movie);
        Result DeleteById(Guid id);
    }
}
