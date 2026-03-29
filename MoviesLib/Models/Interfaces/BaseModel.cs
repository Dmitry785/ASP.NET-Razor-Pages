namespace Domain.Models.Interfaces
{
    public abstract class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
