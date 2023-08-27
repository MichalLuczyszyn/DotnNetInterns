using Playground.Core.Entities.Reservations;
using Playground.Core.Entities.Rooms;
using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Core.Entities.DeskSpots;

public class DeskSpot : BaseEntity
{
    internal DeskSpotId Id { get; private set; }
    internal Name Name { get; private set; }
    internal RoomId RoomId { get; private set; }
    internal Room Room { get; private set; }
    internal UserId? DefaultEmployeeId { get; private set; } // When default employee is assigned, given desk spot will be available only to this employee unless he will be absent
    internal IEnumerable<Reservation> Reservations => _reservations;
    private readonly HashSet<Reservation> _reservations = new();

    private DeskSpot()
    {
        
    }
    internal static DeskSpot CreateDeskSpot(DeskSpotId id, Name name, RoomId roomId)
    {
        return new DeskSpot()
        {
            Id = id,
            Name = name,
            RoomId = roomId
        };
    }
}