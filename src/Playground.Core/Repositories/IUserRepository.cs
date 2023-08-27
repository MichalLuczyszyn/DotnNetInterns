using Playground.Core.Entities.Users;
using Playground.Core.ValueObjects;

namespace Playground.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(Email email);
    Task<User?> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
}