using NTHackathon.Domain.Models;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared;

public class GetEntityByIdSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : class, IBaseEntity
{
    public GetEntityByIdSpecification(int id) : base(e => e.Id == id)
    {
    }
}
