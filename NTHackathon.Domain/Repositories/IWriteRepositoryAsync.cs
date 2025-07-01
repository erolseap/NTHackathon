using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Repositories;

public interface IWriteRepositoryAsync<TEntity> : IReadOnlyRepositoryAsync<TEntity>
    where TEntity : class, IBaseEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(params TEntity[] entities);
    void Update(TEntity entity);
    void UpdateRange(params TEntity[] entities);
    void Remove(TEntity entity);
    void RemoveRange(params TEntity[] entities);
}
