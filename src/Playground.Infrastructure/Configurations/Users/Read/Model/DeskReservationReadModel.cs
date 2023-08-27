namespace Playground.Infrastructure.Configurations.Users.Read.Model;

public class DeskReservationReadModel : ReservationReadModel
{
    public Guid UserId { get; set; }
    public string EmployeeName { get; set; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
}