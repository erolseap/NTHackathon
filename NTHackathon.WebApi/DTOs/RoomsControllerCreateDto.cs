using NTHackathon.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NTHackathon.WebApi.DTOs;

public class RoomsControllerCreateDto
{
    [Required]
    public int? Number { get; init; }
    [Required]
    public RoomType? Type { get; init; }
    [Required]
    public decimal? PricePerNight { get; init; }
    [Required]
    public bool? IsReserved { get; init; }
    [Required]
    public IFormFile? Image { get; init; }
}
