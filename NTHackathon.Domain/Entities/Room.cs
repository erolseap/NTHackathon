using NTHackathon.Domain.Enums;
using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Entities;

public class Room : BaseEntity
{
    /**
     * Properties
     */

    public int Number { get; set; }
    public RoomType Type { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsReserved { get; set; }
    public string ImageUrl { get; set; }

    /**
     * Relations
     */

    public ICollection<Reservation> Reservations { get; set; } = [];
}
