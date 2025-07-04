namespace NTHackathon.Application.Repositories;

public interface IUnitOfWork
{ 
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}