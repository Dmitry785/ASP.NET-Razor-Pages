using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Models;

namespace Application.Services.Interfaces
{
    internal interface IMovieService
    {
        Result<List<Movie>> GetAllMovies();
        Result<Movie> GetById(Guid id);
        Result<Guid> Create(Movie movie);
        Result Update(Movie movie);
        Result DeleteById(Guid id);
    }
}
