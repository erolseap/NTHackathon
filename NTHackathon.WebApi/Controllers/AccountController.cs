using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Infrastructure.Entities;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ManagedControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    [Authorize]
    [HttpGet("",  Name = "Get account information")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAccountInfo()
    {
        var user = (await _userManager.GetUserAsync(User))!;
        return Ok(new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
        });
    }
}
