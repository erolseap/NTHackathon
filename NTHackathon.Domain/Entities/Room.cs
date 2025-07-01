namespace NTHackathon.Domain.Entities;

public class Room
{
   public ICollection<Reservation> Reservations { get; set; } = [];
}