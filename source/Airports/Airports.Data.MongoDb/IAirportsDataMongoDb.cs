namespace Airports.Data.MongoDb
{
    using Airports.Models;
    using System.Linq;

    public interface IAirportsDataMongoDb
    {
        IQueryable<Airport> Airports { get; }

        IQueryable<Airline> Airlines { get; }

        IQueryable<Flight> Flights { get; }

        IQueryable<City> Cities { get; }

        IQueryable<Country> Countries { get; }

        void Save<T>(T item);
    }
}
