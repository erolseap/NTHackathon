namespace NTHackathon.Domain.Entities;

public class Customer
{
   public ICollection<Reservation> Reservations { get; set; } = [];
}