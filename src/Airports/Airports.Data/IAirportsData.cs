namespace Airports.Data
{
    using Airports.Data.Repositories;
    using Airports.Models;

    public interface IAirportsData
    {
        AirportsRepository Airports { get; }

        AirlinesRepository Airlines { get; }

        FlightsRepository Flights { get; }

        IRepository<City> Cities { get; }

        IRepository<Country> Countries { get; }

        void SaveChanges();
    }
}
