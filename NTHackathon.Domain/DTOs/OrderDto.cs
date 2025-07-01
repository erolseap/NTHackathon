namespace NTHackathon.Domain.DTOs;

public class OrderDto
{
    public int Id { get; init; }
    public string Password { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string HppUrl { get; init; } = null!;
    public string Cvv2AuthStatus { get; init; } = null!;
    public string Secret { get; init; } = null!;
}
