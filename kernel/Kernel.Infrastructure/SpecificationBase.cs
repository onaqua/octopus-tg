using Octopus.Kernel.Domain.Entities;
using System.Linq.Expressions;

namespace Octopus.Kernel.Infrastructure;

/// <summary>
/// Base class for specifications.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class SpecificationBase<TEntity>
    where TEntity : IEntity
{
    protected SpecificationBase(Expression<Func<TEntity, bool>>? criteria) =>
         Criteria = criteria;

    public bool IsQueryFiltersIgnored { get; private set; }

    public Expression<Func<TEntity, bool>>? Criteria { get; set; }

    public List<Expression<Func<TEntity, object?>>> IncludeExpressions { get; } = new();

    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    protected void Include(Expression<Func<TEntity, object?>> includeExpressions) =>
        IncludeExpressions.Add(includeExpressions);

    protected void OrderBy(
        Expression<Func<TEntity, object>> orderByExpressions) =>
        OrderByExpression = orderByExpressions;

    protected void OrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;

    protected void IgnoreQueryFilters() =>
        IsQueryFiltersIgnored = true;
}
