using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Application.DTO;
using Playground.Application.Users.GetUser;
using Playground.Infrastructure.Contexts;
using Playground.Infrastructure.Queries.Helpers;

namespace Playground.Infrastructure.Queries;

internal sealed class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly WriteDbContext _writeDbContext;
    private readonly ReadDbContext _dbContext;

    public GetUserHandler(ReadDbContext dbContext, WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
        _dbContext = dbContext;
    }

    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken: cancellationToken) ?? throw new Exception("User doesn't exist");

        return user.AsDto();
    }
}