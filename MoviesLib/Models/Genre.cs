using Domain.Models.Interfaces;

namespace Domain.Models
{
    public class Genre : BaseModel<Guid>
    {
        public string Name { get; set; }
        public Genre()
        {

        }
        public Genre(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
