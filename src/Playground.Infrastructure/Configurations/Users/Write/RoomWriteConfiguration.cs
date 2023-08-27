using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Entities.Rooms;
using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Infrastructure.Configurations.Users.Write;

public class RoomWriteConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new RoomId(x))
            .IsRequired();
        
        builder.Property(x => x.Status).IsRequired().HasDefaultValue(EntryStatus.Active);
        
        builder.Property(x => x.Name).HasConversion(x => x.Value, x => new Name(x))
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.Floor)
            .IsRequired();

        builder.HasMany(x => x.Spots)
            .WithOne()
            .HasForeignKey("RoomId")
            .IsRequired();
        
        builder.ToTable("Rooms");
    }
}