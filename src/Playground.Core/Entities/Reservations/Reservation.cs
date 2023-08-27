using Playground.Core.Shared;
using Playground.Core.ValueObjects;

namespace Playground.Core.Entities.Reservations;

public abstract class Reservation : BaseEntity
{
    internal ReservationId Id { get; }
    internal Date Date { get; private set; }

    protected Reservation()
    {
    }

    protected Reservation(ReservationId id, Date date)
    {
        Id = id;
        Date = date;
    }
}