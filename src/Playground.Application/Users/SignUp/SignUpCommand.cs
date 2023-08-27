using System.Text.Json.Serialization;
using MediatR;

namespace Playground.Application.Users.SignUp;

public record SignUpCommand(string Email, string Username, string Password, string FullName, string Role) : IRequest
{
   [JsonIgnore] public Guid UserId { get; set; }
}
