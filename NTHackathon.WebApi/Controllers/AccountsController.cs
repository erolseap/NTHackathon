using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTHackathon.Infrastructure.Entities;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ManagedControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AccountsController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("",  Name = "Get all accounts information")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAccountsInfo()
    {
        var users = await _userManager.Users.Select(user => new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            IsAdmin = false
        }).ToListAsync();
        return Ok(users);
    }
}