using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Entities;

public class Reservation : BaseEntity
{
    /**
     * Properties
     */

    public int CustomerId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    
    /**
     * Relations
     */

    public Room? Room { get; set; }
    public ServiceEntity? Service { get; set; }
}
