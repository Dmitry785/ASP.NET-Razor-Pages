using Domain.Models.Interfaces;

namespace Domain.Models
{
    public class Movie : BaseModel<Guid>
    {
        public string Name { get; set; }
        public Director Director { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public List<Schedule> Schedules { get; set; }
        public Movie()
        {

        }
        public Movie(string name, string desc, Director director, Genre genre)
        {
            Name = name;
            Director = director;
            Genre = genre;
            Description = desc;
            Schedules = new List<Schedule>();
        }


    }
}
