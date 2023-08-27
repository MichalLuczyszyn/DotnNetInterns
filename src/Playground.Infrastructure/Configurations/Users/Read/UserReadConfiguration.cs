using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Shared;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Configurations.Users.Read;

public class UserReadConfiguration : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ToTable("Users");
    }
}