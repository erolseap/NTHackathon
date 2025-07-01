using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTHackathon.Domain.Entities;

namespace NTHackathon.Infrastructure.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(s => s.Price);
        builder.Property(s => s.ReservationId);

        builder.HasOne(s => s.Reservation)
            .WithOne(rsrv => rsrv.Service);
    }
}
