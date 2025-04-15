using Octopus.Constructor.Domain.Entities;

namespace Octopus.Constructor.Domain;

public abstract class EntityBase<TKey> : IEntity where TKey : IEquatable<TKey>
{
    public TKey Id { get; protected set; } = default!;
}

