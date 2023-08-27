using Playground.Core.Shared;

namespace Playground.Infrastructure.Configurations.Users.Read.Model;

public class DeskSpotReadModel
{
    public Guid Id { get; set; }
    public EntryStatus Status { get;  set; }
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public RoomReadModel Room { get; set; }
    public Guid? DefaultEmployeeId { get; set; }
    public IEnumerable<DeskReservationReadModel> Reservations { get; set; }
}