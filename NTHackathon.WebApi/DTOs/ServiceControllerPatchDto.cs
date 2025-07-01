using System.ComponentModel.DataAnnotations;

namespace NTHackathon.WebApi.DTOs;

public class ServiceControllerPatchDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public decimal? Price { get; set; }
    [Required]
    public int? ReservationId { get; set; }
}
