namespace Airports.Data.SqlServer
{
    using Airports.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IAirportsDbContextSqlServer
    {
        IDbSet<Airline> Airlines { get; set; }

        IDbSet<Airport> Airports { get; set; }

        IDbSet<Flight> Flights { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<T> GetDataSet<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
