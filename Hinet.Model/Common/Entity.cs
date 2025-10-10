namespace Hinet.Model
{
    public abstract class BaseEntity
    {
        public static bool operator ~(BaseEntity value) => value != null;
    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}