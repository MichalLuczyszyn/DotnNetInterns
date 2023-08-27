using MediatR;
using Playground.Application.DTO;

namespace Playground.Application.Users.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }
}