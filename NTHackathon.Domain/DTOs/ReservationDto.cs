namespace NTHackathon.Domain.DTOs;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }
    public CustomerDto? Customer { get; init; }
    public RoomDto? Room { get; init; }
}
