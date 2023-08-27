using Playground.Core.Shared;

namespace Playground.Infrastructure.Configurations.Users.Read.Model;

public class RoomReadModel
{
    public Guid Id { get; set; }
    public EntryStatus Status { get;  set; }
    public string Name { get; set; }
    public int Floor { get; set; }
    
    public ICollection<DeskSpotReadModel> Spots { get; set; }
}