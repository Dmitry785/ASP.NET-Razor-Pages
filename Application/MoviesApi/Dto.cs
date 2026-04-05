using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interfaces;
using Domain.Models;

namespace Application.MoviesApi
{
    public class MovieData
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string? Poster { get; set; }
        public int Year { get; set; }
        public MovieData(string name, string desc, int year, string director, string genre, string? poster = null)
        {
            Name = name;
            Director = director;
            Year = year;
            Genre = genre;
            Description = desc;
            Poster = poster;
        }


    }
}
