using Octopus.Constructor.Domain.Entities;

namespace Octopus.Constructor.Domain.Repositories;

/// <summary>
/// Write repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IWriteRepository<in TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Add entity to repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}