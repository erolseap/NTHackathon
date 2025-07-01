using Microsoft.AspNetCore.Identity;
using NTHackathon.Domain.Models;

namespace NTHackathon.Infrastructure.Entities;

public class AppUserRole : IdentityRole<int>, IAppUserRole
{
    public AppUserRole() : base()
    {
        
    }

    public AppUserRole(string name) : base(name)
    {
    }
}
