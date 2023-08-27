using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Shared;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Configurations.Users.Read;

public class RoomReadConfiguration : IEntityTypeConfiguration<RoomReadModel>
{
    public void Configure(EntityTypeBuilder<RoomReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ToTable("Rooms");
    }
}