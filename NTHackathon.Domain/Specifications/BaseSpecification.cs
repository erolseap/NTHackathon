using System.Linq.Expressions;
using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Specifications;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class, IBaseEntity
{
    public Expression<Func<TEntity, bool>> Criteria { get; }
    public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
    public IReadOnlyCollection<string> IncludeStrings { get; } = new List<string>();
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; } = null;
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; } = null;
    public Expression<Func<TEntity, object>>? GroupBy { get; private set; } = null;
    public int? TakeCount { get; private set; } = null;
    public int? SkipCount { get; private set; } = null;

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        ((List<Expression<Func<TEntity, object>>>)Includes).Add(includeExpression);
    }

    public virtual void AddInclude(string includeString)
    {
        ((List<string>)IncludeStrings).Add(includeString);
    }

    public virtual void Skip(int skip)
    {
        SkipCount = skip;
    }

    public virtual void Take(int take)
    {
        TakeCount = take;
    }

    public virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    public virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
}
