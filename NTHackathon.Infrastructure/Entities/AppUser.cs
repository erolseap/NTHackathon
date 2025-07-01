using Microsoft.AspNetCore.Identity;
using NTHackathon.Domain.Models;

namespace NTHackathon.Infrastructure.Entities;

public class AppUser : IdentityUser<int>, IAppUser
{
    public AppUser() : base()
    {
        
    }

    public AppUser(string userName) : base(userName)
    {
    }
}
