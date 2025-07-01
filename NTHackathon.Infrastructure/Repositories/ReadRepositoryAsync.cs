using NTHackathon.Domain.Models;
using NTHackathon.Domain.Repositories;
using NTHackathon.Domain.Specifications;
using NTHackathon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NTHackathon.Infrastructure.Repositories;

public class ReadOnlyRepositoryAsync<TEntity> : IReadOnlyRepositoryAsync<TEntity>
    where TEntity : class, IBaseEntity
{
    private readonly AppDbContext _context;

    protected virtual DbSet<TEntity> DbEntitySet => _context.Set<TEntity>();
    protected virtual IQueryable<TEntity> DbSetQueryable => DbEntitySet.AsNoTracking();

    public ReadOnlyRepositoryAsync(AppDbContext context)
    {
        _context = context;
    }

    public virtual bool IsTrackingCapable => false;

    public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity>? spec = null,
        CancellationToken cancellationToken = default)
    {
        return await (spec == null ? DbSetQueryable : ApplySpecification(spec)).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<TEntity>? spec = null,
        CancellationToken cancellationToken = default)
    {
        return await (spec == null ? DbSetQueryable : ApplySpecification(spec)).CountAsync(cancellationToken);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return DbSetQueryable.EvaluateSpecification(spec);
    }
}