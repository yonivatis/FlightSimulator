using Microsoft.EntityFrameworkCore;
using ServerAirportFinal.BL.StationBL;
using ServerAirportFinal.Models;

namespace ServerAirportFinal.Data
{
    public class AirportContext : DbContext
    {
        // public DbSet<AirplaneModel> Airplanes { get; set; } //unnecessary- an airplane can appear more than once (a flight can't)
        public DbSet<ProcessStatusModel> processStatusModels { get; set; }
        public DbSet<AirportImage> AirportImages { get; set; }
        public DbSet<StationModel> Stations { get; set; }
        public DbSet<FlightModel> Flights { get; set; }
        public AirportContext(DbContextOptions<AirportContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StationModel>().HasData(
           new StationModel { Type = StationType.LandingRequest, StationId = 1, IsFree = true },
                new StationModel { Type = StationType.LandingPreparation, StationId = 2, IsFree = true },
                new StationModel { Type = StationType.Approach, StationId = 3, IsFree = true },
                new StationModel { Type = StationType.Runway, StationId = 4, IsFree = true },
                new StationModel { Type = StationType.Transportaion, StationId = 5, IsFree = true },
                new StationModel { Type = StationType.Load, StationId = 6, IsFree = true },
                new StationModel { Type = StationType.Load, StationId = 7, IsFree = true },
                new StationModel { Type = StationType.Transportaion, StationId = 8, IsFree = true }
           );
            modelBuilder.Entity<FlightModel>()
                .Property(x => x.FlightId)
                .UseIdentityColumn(seed: 100, increment: 1);

            //modelBuilder.Entity<AirportImage>().HasData(new AirportImage { ImageId = 1, ImageTime = DateTime.Now });
            //modelBuilder.Entity<ProcessStatusModel>().HasNoKey();
        }
    }
}
