using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Playground.Core.Entities.Reservations;
using Playground.Infrastructure.Configurations.Users.Read;
using Playground.Infrastructure.Configurations.Users.Read.Model;

namespace Playground.Infrastructure.Contexts;

public class ReadDbContext : DbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<DeskSpotReadModel> DeskSpots { get; set; }
    public DbSet<DeskReservationReadModel> DeskReservations { get; set; }
    public DbSet<ReservationReadModel> Reservations { get; set; }
    public DbSet<RoomReadModel> Rooms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DeskReservationReadConfiguration());
        modelBuilder.ApplyConfiguration(new DeskSpotReadConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationReadConfiguration());
        modelBuilder.ApplyConfiguration(new RoomReadConfiguration());
        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
    }
}