using NTHackathon.Domain.Models;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Domain.Repositories;

public interface IReadOnlyRepositoryAsync<TEntity>
    where TEntity : class, IBaseEntity
{
    public bool IsTrackingCapable { get; }

    Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity>? spec = null, CancellationToken cancellationToken = default);
    Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<TEntity>? spec = null, CancellationToken cancellationToken = default);
}
