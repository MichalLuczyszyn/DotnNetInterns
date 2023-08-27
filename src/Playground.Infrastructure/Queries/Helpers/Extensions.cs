using Playground.Application.DTO;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Queries.Helpers;

public static class Extensions
{
    public static UserDto AsDto(this UserReadModel entity)
        => new()
        {
            Id = entity.Id,
            Username = entity.Username,
            FullName = entity.FullName
        };
}