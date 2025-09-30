namespace Hinet.Model
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}