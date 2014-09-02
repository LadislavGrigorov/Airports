namespace Airports.Data.SqlServer
{
    using Airports.Data.SqlServer.Repositories;
    using Airports.Models;

    public interface IAirportsDataSqlServer
    {
        AirportsRepository Airports { get; }

        AirlinesRepository Airlines { get; }

        FlightsRepository Flights { get; }

        IRepository<City> Cities { get; }

        IRepository<Country> Countries { get; }

        void SaveChanges();
    }
}
