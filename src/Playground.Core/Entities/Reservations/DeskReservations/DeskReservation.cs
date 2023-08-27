using Playground.Core.ValueObjects;

namespace Playground.Core.Entities.Reservations.DeskReservations;

public class DeskReservation : Reservation
{
    internal UserId UserId { get; private set; }
    public Username EmployeeName { get; private set; }
    internal Date From { get; private set; }
    internal Date To { get; private set; }

    private DeskReservation()
    {
    }

    internal DeskReservation(ReservationId reservationId, UserId userId, string employeeName, Date date) : base(reservationId,
        date)
    {
        UserId = userId;
        EmployeeName = employeeName;
    }

    internal static DeskReservation CreateReservation(ReservationId reservationId, UserId userId, string employeeName, Date date)
    {
        return new DeskReservation(reservationId, userId, employeeName, date);
    }
}