namespace NTHackathon.Domain.DTOs;

public class PaymentCreateDto
{
    public decimal Amount { get; init; }
    public string Description { get; init; } = null!;
}
