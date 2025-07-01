namespace NTHackathon.Domain.DTOs;

public class ServiceDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int ReservationId { get; init; }
}