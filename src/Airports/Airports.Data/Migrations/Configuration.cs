namespace Airports.Data.Migrations
{
    using Airports.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    public sealed class Configuration : DbMigrationsConfiguration<AirportsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Airports.Data.AirportsDbContext context)
        {
            using (AirportsDbContext dbContext = new AirportsDbContext())
            {
                var airportSofia = new Airport()
                {
                    Name = "Sofia Airport",
                    AirportCode = "SOF",
                    City = new City() { Name = "Sofia", Country = new Country() { Name = "Bulgaria" } }
                };

                var berlinTegel = new Airport() 
                { 
                    Name = "Berlin Tegel", 
                    AirportCode = "BER",
                    City = new City() { Name = "Berlin", Country = new Country() { Name = "Germany" } }
                };

                var berSof = new Flight()
                {
                    Airline = new Airline() { Name = "Lufthansa" },
                    ArrivalAirport = airportSofia,
                    DepartureAirport = berlinTegel,
                    DurationHours = 1.0,
                    FlightDate = DateTime.Now.AddHours(2),
                    FlightCode = "6BC42"
                };

                var sofBer = new Flight()
                {
                    Airline = new Airline() { Name = "Bulgaria Air" },
                    ArrivalAirport = berlinTegel,
                    DepartureAirport = airportSofia,
                    DurationHours = 2.0,
                    FlightDate = DateTime.Now,
                    FlightCode = "AAA42"
                };

                dbContext.Flights.AddOrUpdate(f => f.FlightCode, sofBer, berSof);

                dbContext.SaveChanges();
            }
        }
    }
}
