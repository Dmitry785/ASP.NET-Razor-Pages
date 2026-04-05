using Domain.Models.Interfaces;

namespace Domain.Models
{
    public class Director : BaseModel<Guid>
    {
        public string FullName { get; set; }
        public Director()
        {

        }
        public Director(string fullName)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
        }
    }
}
