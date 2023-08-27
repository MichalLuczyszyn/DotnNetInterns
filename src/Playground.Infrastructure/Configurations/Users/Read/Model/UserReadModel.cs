using Playground.Core.Shared;

namespace Playground.Infrastructure.Configurations.Users.Read.Model;

public class UserReadModel
{
    public Guid Id { get; private set; }
    public EntryStatus Status { get;  set; }

    public string Email { get; private set; }
    public string Username { get; private set; }

    public string FullName { get; private set; }
    public string Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
}