using Domain.Models.Interfaces;

namespace Domain.Models
{
    public class Schedule : BaseModel<Guid>
    {
        public DateTime SessionTime { get; set; }
        public Movie Movie { get; set; }
        public Schedule()
        {

        }
        public Schedule(DateTime dateTime)
        {
        }
        public Schedule(DateTime dateTime, Movie movie)
        {
            Id = Guid.NewGuid();
            SessionTime = dateTime;
            Movie = movie;
        }
    }
}
