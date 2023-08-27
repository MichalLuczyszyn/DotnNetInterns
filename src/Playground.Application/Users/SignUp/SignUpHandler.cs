using MediatR;
using Playground.Application.Clock;
using Playground.Application.Exceptions;
using Playground.Application.Security;
using Playground.Core.Entities.Users;
using Playground.Core.Repositories;
using Playground.Core.ValueObjects;
namespace Playground.Application.Users.SignUp;

internal sealed class SignUpHandler : IRequestHandler<SignUpCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var fullName = new FullName(command.FullName);
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
            throw new EmailAlreadyInUseException(email);
        

        if (await _userRepository.GetByUsernameAsync(username) is not null)
            throw new UsernameAlreadyInUseException(username);
        

        var securedPassword = _passwordManager.Secure(password);
        var user = User.CreateUser(userId, email, username, securedPassword, fullName, role, _clock.Current());
        await _userRepository.AddAsync(user);
    }
}