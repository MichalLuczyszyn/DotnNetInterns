using MediatR;
using Playground.Application.Exceptions;
using Playground.Application.Security;
using Playground.Core.Repositories;

namespace Playground.Application.Users.SignIn;

internal sealed class SignInHandler : IRequestHandler<SignInCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;

    public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager,
        ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }

    public async Task Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(command.Password, user.UserPassword))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(user.UserId, user.UserRole);
        _tokenStorage.Set(jwt);

        return;
    }
}