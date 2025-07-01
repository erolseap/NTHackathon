using Microsoft.EntityFrameworkCore;
using NTHackathon.Domain.Models;
using NTHackathon.Domain.Repositories;
using NTHackathon.Infrastructure.Data;
using NTHackathon.Infrastructure.Repositories;

namespace NTHackathon.Infrastructure.Repositories;

public class WriteRepositoryAsync<TEntity> : ReadOnlyRepositoryAsync<TEntity>, IWriteRepositoryAsync<TEntity>
    where TEntity : class, IBaseEntity
{
    protected override IQueryable<TEntity> DbSetQueryable => DbEntitySet.AsTracking();

    public override bool IsTrackingCapable => true;

    public WriteRepositoryAsync(AppDbContext context) : base(context)
    {
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbEntitySet.AddAsync(entity,  cancellationToken);
        return entity;
    }

    public async Task AddRangeAsync(params TEntity[] entities)
    {
        await DbEntitySet.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        DbEntitySet.Update(entity);
    }

    public void UpdateRange(params TEntity[] entities)
    {
        DbEntitySet.UpdateRange(entities);
    }

    public void Remove(TEntity entity)
    {
        DbEntitySet.Remove(entity);
    }

    public void RemoveRange(params TEntity[] entities)
    {
        DbEntitySet.RemoveRange(entities);
    }
}
