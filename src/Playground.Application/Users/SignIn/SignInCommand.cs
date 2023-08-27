using MediatR;

namespace Playground.Application.Users.SignIn;

public record SignInCommand(string Email, string Password) : IRequest;
