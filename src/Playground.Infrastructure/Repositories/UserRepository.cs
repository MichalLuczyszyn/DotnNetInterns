using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities.Users;
using Playground.Core.Repositories;
using Playground.Core.ValueObjects;
using Playground.Infrastructure.Contexts;

namespace Playground.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly WriteDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = dbContext.Users;
    }

    public Task<User?> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User?> GetByUsernameAsync(Username username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}