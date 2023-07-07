namespace Domain.Primitive;
public abstract class Entity<T> : IEquatable<Entity<T>>
{
    protected Entity(T id)
    {
        Id = id;
    }

    public T Id { get; private init; }

    public static bool operator ==(Entity<T>? first, Entity<T>? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity<T>? first, Entity<T>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity<T> entity)
        {
            return false;
        }

        return EqualityComparer<T>.Default.Equals(entity.Id, Id);
    }

    public bool Equals(Entity<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return EqualityComparer<T>.Default.Equals(other.Id, Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Id) * 41;
    }
}
