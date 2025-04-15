using Microsoft.EntityFrameworkCore;
using Octopus.Kernel.Domain.Entities;

namespace Octopus.Kernel.Infrastructure;

public abstract class RepositoryBase<TEntity>(DbSet<TEntity> set)
    where TEntity : class, IEntity
{
    public DbSet<TEntity> Set { get; } = set;

    protected IQueryable<TEntity> UseSpecification(
       SpecificationBase<TEntity> specification)
    {
        return SpecificationEvaluator.GetQuery(
           Set,
           specification);
    }
}
