using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Entities.Reservations.DeskReservations;
using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Infrastructure.Configurations.Users.Write;

public class DeskReservationWriteConfiguration : IEntityTypeConfiguration<DeskReservation>
{
    public void Configure(EntityTypeBuilder<DeskReservation> builder)
    {
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.Status).IsRequired().HasDefaultValue(EntryStatus.Active);
        
        builder.Property(x => x.EmployeeName)
            .HasConversion(x => x.Value, x => new Username(x))
            .IsRequired();

        builder.Property(x => x.From)
            .IsRequired()
            .HasConversion(x => x.Value, x => new Date(x)); 
        builder.Property(x => x.To)
            .IsRequired()
            .HasConversion(x => x.Value, x => new Date(x));
        
        builder.ToTable("Reservations");
    }
}