using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Shared;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Configurations.Users.Read;

public class DeskReservationReadConfiguration : IEntityTypeConfiguration<DeskReservationReadModel>
{
    public void Configure(EntityTypeBuilder<DeskReservationReadModel> builder)
    {
        builder.ToTable("Reservations");
    }
}