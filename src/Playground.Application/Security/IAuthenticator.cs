using Playground.Application.Auth;

namespace Playground.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}