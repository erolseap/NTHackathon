using NTHackathon.Domain.Enums;
using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Entities;

public class Room : BaseEntity
{
    public int Number { get; set; }
    public RoomType Type { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsReserved { get; set; }
}




