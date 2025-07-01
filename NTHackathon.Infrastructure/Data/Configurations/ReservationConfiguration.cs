using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTHackathon.Domain.Entities;

namespace NTHackathon.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.CheckInDate);
        builder.Property(r => r.CheckOutDate);
        builder.Property(r => r.CustomerId);
        builder.Property(r => r.RoomId);

        builder.HasOne(p => p.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId);
        builder.HasOne(p => p.Room)
            .WithMany(r => r.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
