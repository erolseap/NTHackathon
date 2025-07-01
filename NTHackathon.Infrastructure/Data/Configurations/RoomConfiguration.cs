using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTHackathon.Domain.Entities;

namespace NTHackathon.Infrastructure.Data.Configurations;
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Number)
            .IsRequired();
        builder.Property(r => r.Type)
            .IsRequired();
        builder.Property(r => r.PricePerNight)
            .IsRequired();
        builder.Property(r => r.IsReserved)
            .IsRequired();
    }
}
