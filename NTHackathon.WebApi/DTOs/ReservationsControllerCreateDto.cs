using System.ComponentModel.DataAnnotations;

namespace NTHackathon.WebApi.DTOs;

public class ReservationsControllerCreateDto
{
    [Required]
    public int? CustomerId { get; init; }
    [Required]
    public int? RoomId { get; init; }
    [Required]
    public DateTime? CheckInDate { get; init; }
    [Required]
    public DateTime? CheckOutDate { get; init; }
}
