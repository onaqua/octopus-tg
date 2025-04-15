using Microsoft.EntityFrameworkCore;
using Octopus.Constructor.Domain.Entities;

namespace Octopus.Constructor.Infrastructure;

public abstract class RepositoryBase<TEntity>(DatabaseContext databaseContext)
    where TEntity : class, IEntity
{
    public DbSet<TEntity> Set { get; } = databaseContext.Set<TEntity>();

    protected IQueryable<TEntity> UseSpecification(
       SpecificationBase<TEntity> specification)
    {
        return SpecificationEvaluator.GetQuery(
           Set,
           specification);
    }
}
