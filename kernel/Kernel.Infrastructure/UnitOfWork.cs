using Microsoft.EntityFrameworkCore;
using Octopus.Kernel.Domain;

namespace Octopus.Kernel.Infrastructure;

public class UnitOfWork(DbContext databaseContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => databaseContext.SaveChangesAsync(cancellationToken);
}