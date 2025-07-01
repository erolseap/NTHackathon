using System.Linq.Expressions;
using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Specifications;

public interface ISpecification<TEntity>
    where TEntity : class, IBaseEntity
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }
    IReadOnlyCollection<string> IncludeStrings { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDescending { get; }
    Expression<Func<TEntity, object>>? GroupBy { get; }
    int? TakeCount { get; }
    int? SkipCount { get; }
}
