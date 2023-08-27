using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Configurations.Users.Read;

public class ReservationReadConfiguration: IEntityTypeConfiguration<ReservationReadModel>
{
    public void Configure(EntityTypeBuilder<ReservationReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Reservations");
    }
}