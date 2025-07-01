using NTHackathon.Domain.Models;

namespace NTHackathon.Domain.Entities;

public class ServiceEntity : BaseEntity
{
    /**
     * Properties
     */

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int ReservationId { get; set; }
    
    /**
     * Relations
     */
    
    public Reservation? Reservation { get; set; }
}