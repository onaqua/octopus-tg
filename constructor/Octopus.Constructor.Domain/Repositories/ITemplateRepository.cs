using Octopus.Kernel.Domain.Repositories;

namespace Octopus.Constructor.Domain.Repositories;

/// <summary>
/// Templates write only repository
/// </summary>
public interface ITemplateWriteRepository : IWriteRepository<Template>;

/// <summary>
/// Templates read only repository
/// </summary>
public interface ITemplateReadRepository : IReadRepository<Guid, Template>
{
    /// <summary>
    /// Check if template exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default);
}

/// <summary>
/// Templates repository
/// </summary>
public interface ITemplateRepository : ITemplateWriteRepository, ITemplateReadRepository;