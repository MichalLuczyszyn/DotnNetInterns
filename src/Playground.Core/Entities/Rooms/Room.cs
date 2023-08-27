using Playground.Core.Entities.DeskSpots;
using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Core.Entities.Rooms;

public class Room : BaseEntity
{
    internal RoomId Id { get; private set; }
    internal Name Name { get; private set; }
    internal int Floor { get; private set; }

    public IEnumerable<DeskSpot> Spots => _deskSpots;
    private readonly HashSet<DeskSpot> _deskSpots = new();

    private Room()
    {
    }

    public static Room CreateRoom(RoomId roomId, Name name, int floor)
    {
        return new Room()
        {
            Id = roomId,
            Name = name,
            Floor = floor
        };
    }

    public DeskSpot AddDesk(DeskSpotId id, Name name)
    {
        var desk = DeskSpot.CreateDeskSpot(id, name, Id);
        _deskSpots.Add(desk);

        return desk;
    }
}