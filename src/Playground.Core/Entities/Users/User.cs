using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Core.Entities.Users;

public class User : BaseEntity
{
    internal UserId Id { get; private init; }
    public Guid UserId => Id;
    internal Email Email { get; private set; }
    internal Username Username { get; private set; }
    internal Password Password { get; private set; }
    public string UserPassword => Password;
    internal FullName FullName { get; private set; }
    internal Role Role { get; private set; }
    public string UserRole => Role;
    internal DateTime CreatedAt { get; private set; }

    private User()
    {
        
    }
    public static User CreateUser(UserId id, Email email, Username username, Password password, FullName fullName,
        Role role,
        DateTime createdAt)
    {
        return new User()
        {
            Id = id,
            Email = email,
            Username = username,
            Password = password,
            FullName = fullName,
            Role = role,
            CreatedAt = createdAt,
        };
    }
}