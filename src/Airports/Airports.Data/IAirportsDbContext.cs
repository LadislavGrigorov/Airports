namespace Airports.Data
{
    using Airports.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IAirportsDbContext
    {
        DbSet<Airline> Airlines { get; set; }

        DbSet<Airport> Airports { get; set; }

        DbSet<Flight> Flights { get; set; }

        DbSet<City> Cities { get; set; }

        DbSet<Country> Countries { get; set; }

        IDbSet<T> GetDataSet<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
