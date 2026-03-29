using Domain.Models.Interfaces;

namespace Domain.Models
{
    public class Director : BaseModel<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Director()
        {

        }
        public Director(string fname, string lname)
        {
            Id = Guid.NewGuid();
            FirstName = fname;
            LastName = lname;
        }
    }
}
