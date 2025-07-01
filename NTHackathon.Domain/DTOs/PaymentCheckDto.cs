using NTHackathon.Domain.Enums;

namespace NTHackathon.Domain.DTOs;

public class PaymentCheckDto
{
    public string Token { get; init; } = null!;
    public int Id { get; init; }
    public PaymentStatus Status { get; init; }
}
