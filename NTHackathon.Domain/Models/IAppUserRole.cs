namespace NTHackathon.Domain.Models;

public interface IAppUserRole : IBaseEntity
{
    public string? Name { get; set; }
}
