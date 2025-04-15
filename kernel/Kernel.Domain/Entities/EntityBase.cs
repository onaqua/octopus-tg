namespace Octopus.Kernel.Domain.Entities;

public abstract class EntityBase<TKey> : IEntity where TKey : IEquatable<TKey>
{
    public TKey Id { get; protected set; } = default!;
}

