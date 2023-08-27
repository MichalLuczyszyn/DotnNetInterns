using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Entities.DeskSpots;
using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Infrastructure.Configurations.Users.Write;

public class DeskSpotWriteConfiguration : IEntityTypeConfiguration<DeskSpot>
{
    public void Configure(EntityTypeBuilder<DeskSpot> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new DeskSpotId(x))
            .IsRequired();

        builder.Property(x => x.Name).HasConversion(x => x.Value, x => new Name(x))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.RoomId)
            .IsRequired();

        builder.HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DefaultEmployeeId).HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired(false)
            .HasMaxLength(50);

        builder.HasMany(x => x.Reservations)
            .WithOne()
            .HasForeignKey("DeskSpotId")
            .IsRequired();
        
        builder.ToTable("DeskSpots");
    }
}