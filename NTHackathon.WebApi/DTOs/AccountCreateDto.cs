using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NTHackathon.WebApi.DTOs;

public class AccountCreateDto
{
    [Required]
    [NotNull]
    public string? UserName { get; set; }
    [Required]
    [NotNull]
    public string? Email { get; set; }
    [Required]
    [NotNull]
    public string? Password { get; set; }
}
