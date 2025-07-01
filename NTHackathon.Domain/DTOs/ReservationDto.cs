namespace NTHackathon.Domain.DTOs;

public class ReservationDto
{
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }
    public CustomerDto? Customer { get; init; }
    public RoomDto? Room { get; init; }
}

public class RoomDto
{
}

public class CustomerDto
{
}
