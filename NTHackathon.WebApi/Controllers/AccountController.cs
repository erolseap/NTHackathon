using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
    private readonly IUserStore<AppUser> _userStore;

    public AccountController(UserManager<AppUser> userManager, IUserStore<AppUser> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
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
            IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
        });
    }
    
    [HttpPost("register-username",  Name = "Register a new account with username")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(AccountCreateDto data)
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"MapIdentityApi requires a user store with email support.");
        }
        
        var emailStore = (IUserEmailStore<AppUser>)_userStore;
        var email = data.Email;

        var user = new AppUser();
        await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, data.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
