namespace Airports.Data.Loaders
{
    using Airports.Models;
    using System.Data.Entity.Migrations;

    public class SqlServerDataLoader
    {
        public void LoadFlight(Flight flight, AirportsDbContext context)
        {
            //TODO: Add data validation!
            context.Flights.AddOrUpdate(f => f.FlightCode, flight);
            context.SaveChanges();
        }
    }
}
