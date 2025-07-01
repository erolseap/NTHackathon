using NTHackathon.Domain.Enums;

namespace NTHackathon.Domain.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public int Number { get; init; }
    public RoomType Type { get; init; }
    public decimal PricePerNight { get; init; }
    public bool IsReserved { get; init; }
    public string ImageUrl { get; init; }
}
