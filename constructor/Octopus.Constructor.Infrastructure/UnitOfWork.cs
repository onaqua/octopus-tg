using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Infrastructure;

public class UnitOfWork(DatabaseContext databaseContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => databaseContext.SaveChangesAsync(cancellationToken);
}