using NTHackathon.Domain.Models;
using NTHackathon.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace NTHackathon.Infrastructure.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> EvaluateSpecification<TEntity>(this IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification) where TEntity : class, IBaseEntity
    {
        var query = inputQuery;

        query = query.Where(specification.Criteria);

        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,
            (current, include) => current.Include(include));

        // Apply ordering if expressions are set
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.SkipCount != null)
        {
            query = query.Skip(specification.SkipCount.Value);
        }

        if (specification.TakeCount != null)
        {
            query = query.Take(specification.TakeCount.Value);
        }

        return query;
    }
}
