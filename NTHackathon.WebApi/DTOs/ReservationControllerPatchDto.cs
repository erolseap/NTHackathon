using System.ComponentModel.DataAnnotations;

namespace NTHackathon.WebApi.DTOs;

public class ReservationControllerPatchDto
{
    public int? CustomerId { get; init; }
    public int? RoomId { get; init; }
    public DateTime? CheckInDate { get; init; }
    public DateTime? CheckOutDate { get; init; }
}