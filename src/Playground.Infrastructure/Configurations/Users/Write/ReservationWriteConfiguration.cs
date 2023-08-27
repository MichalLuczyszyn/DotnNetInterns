using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Entities.Reservations;
using Playground.Core.Entities.Reservations.DeskReservations;
using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Infrastructure.Configurations.Users.Write;

public class ReservationWriteConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ReservationId(x))
            .IsRequired();

        builder.Property(x => x.Status).IsRequired().HasDefaultValue(EntryStatus.Active);
        
        builder.Property(x => x.Date)
            .IsRequired()
            .HasConversion(x => x.Value, x => new Date(x));

        builder.HasDiscriminator<string>("ReservationType")
            .HasValue<DeskReservation>("Desk");
        
        builder.ToTable("Reservations");
    }
}