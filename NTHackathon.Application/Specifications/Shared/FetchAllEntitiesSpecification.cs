using NTHackathon.Domain.Models;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared;

public class FetchAllEntitiesSpecification<TEntity> : BaseSpecification<TEntity> where TEntity : class, IBaseEntity
{
    public FetchAllEntitiesSpecification() : base(e => true)
    {
    }
}
