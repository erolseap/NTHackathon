using System.ComponentModel.DataAnnotations;

namespace NTHackathon.WebApi.DTOs;

public class ServicesControllerCreateDto
{
    [Required]
    public string? Name { get; init; }
    [Required]
    public decimal? Price { get; init; }
    [Required]
    public int? ReservationId { get; init; }
}
