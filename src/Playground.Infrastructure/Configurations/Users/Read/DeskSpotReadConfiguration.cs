using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Shared;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Configurations.Users.Read;

public class DeskSpotReadConfiguration : IEntityTypeConfiguration<DeskSpotReadModel>
{
    public void Configure(EntityTypeBuilder<DeskSpotReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ToTable("DeskSpots");
    }
}