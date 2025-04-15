using Octopus.Constructor.Domain.Entities;

namespace Octopus.Constructor.Infrastructure;

/// <summary>
/// Read repository.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IReadRepository<TKey, TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Get entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
}