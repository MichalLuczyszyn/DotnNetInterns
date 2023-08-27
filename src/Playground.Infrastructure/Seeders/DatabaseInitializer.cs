using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playground.Application.Users.SignUp;
using Playground.Core.Entities.Rooms;
using Playground.Core.Globals;
using Playground.Core.Roles;
using Playground.Infrastructure.Contexts;

namespace Playground.Infrastructure.Seeders;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (!await dbContext.Rooms.AnyAsync(cancellationToken: cancellationToken))
        {
            var openSpaceRoom = Room.CreateRoom(GlobalData.OpenSpaceRoomId, "Open space", 2);
            var iglooRoom = Room.CreateRoom(GlobalData.IgloRoomId, "Igloo", 2);
            var academyRoom = Room.CreateRoom(GlobalData.AcademyRoomId, "Pięterko", 1);
            var rooms = new List<Room>() { openSpaceRoom, iglooRoom, academyRoom };

            openSpaceRoom.AddDesk(GlobalData.Desk22Id, "22");
            openSpaceRoom.AddDesk(GlobalData.Desk21Id, "21");
            openSpaceRoom.AddDesk(GlobalData.Desk12Id, "12");

            dbContext.Rooms.AddRange(rooms);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
            
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        if (dbContext.Users.Any()) return;
        var signUpCommand =
            new SignUpCommand("email@gmail.com", "Init", "12345!@#$%MQA", "Default User", SystemRoles.user)
            {
                UserId = GlobalData.DefaultUserId
            };

        await mediator.Send(signUpCommand, cancellationToken);

        return;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}