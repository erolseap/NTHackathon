using NTHackathon.Domain.Enums;

namespace NTHackathon.WebApi.DTOs;

public class RoomsControllerUpdateDto
{
    public int? Number { get; init; }
    public RoomType? Type { get; init; }
    public decimal? PricePerNight { get; init; }
    public bool? IsReserved { get; init; }
}
