namespace Airports.Data
{
    using Airports.Data.Repositories;
    using Airports.Models;
    using System.Data.Entity;
    
    public class AirportsDbContext : DbContext, IAirportsDbContext 
    {
        public AirportsDbContext()
            : base("AirportsDb")
        {
        }

        public IDbSet<Airline> Airlines { get; set; }

        public IDbSet<Airport> Airports { get; set; }

        public IDbSet<Flight> Flights { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                        .HasRequired(f => f.DepartureAirport)
                        .WithMany(a => a.DepartureFlights)
                        .HasForeignKey(f => f.DepartureAirportId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                        .HasRequired(f => f.ArrivalAirport)
                        .WithMany(a => a.ArrivalFlights)
                        .HasForeignKey(f => f.ArrivalAirportId)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }


        public IDbSet<T> GetDataSet<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
