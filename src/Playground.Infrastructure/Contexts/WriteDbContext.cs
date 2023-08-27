using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities.DeskSpots;
using Playground.Core.Entities.Reservations;
using Playground.Core.Entities.Reservations.DeskReservations;
using Playground.Core.Entities.Rooms;
using Playground.Core.Entities.Users;
using Playground.Infrastructure.Configurations.Users.Write;

namespace Playground.Infrastructure.Contexts;

public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<DeskSpot> DeskSpots { get; set; }
    public DbSet<DeskReservation> DeskReservations { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DeskReservationWriteConfiguration());
        modelBuilder.ApplyConfiguration(new DeskSpotWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationWriteConfiguration());
        modelBuilder.ApplyConfiguration(new RoomWriteConfiguration());
        modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
    }
}