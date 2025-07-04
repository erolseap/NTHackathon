namespace NTHackathon.WebApi.DTOs;

public class UserDto
{
    public int Id { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public bool EmailConfirmed { get; init; }
    public bool TwoFactorEnabled { get; init; }
    public bool IsAdmin { get; init; }
}
