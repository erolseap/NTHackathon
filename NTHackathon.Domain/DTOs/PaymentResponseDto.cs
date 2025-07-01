namespace NTHackathon.Domain.DTOs;

public class PaymentResponseDto
{
    public OrderDto Order { get; init; } = null!;
    public int Id { get; init; }
}
