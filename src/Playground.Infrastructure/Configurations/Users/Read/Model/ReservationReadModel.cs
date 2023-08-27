using Playground.Core.Shared;

namespace Playground.Infrastructure.Configurations.Users.Read.Model;

public class ReservationReadModel
{
    public Guid Id { get; }
    public EntryStatus Status { get;  set; }
    public DateTimeOffset Date { get; set; }
}