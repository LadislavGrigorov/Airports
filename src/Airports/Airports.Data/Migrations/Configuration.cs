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
                    Location = new Location() { City = "Sofia", Country = "Bulgaria" }
                };

                var berlinTegel = new Airport() 
                { 
                    Name = "Berlin Tegel", 
                    AirportCode = "BER", 
                    Location = new Location() { City = "Berlin", Country = "Germany"}
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

                berlinTegel.DepartureFlights.Add(berSof);
                airportSofia.ArrivalFlights.Add(berSof);
                
                airportSofia.DepartureFlights.Add(sofBer);
                berlinTegel.ArrivalFlights.Add(sofBer);

                dbContext.Airports.AddOrUpdate(a => a.AirportCode, airportSofia, berlinTegel);

                dbContext.SaveChanges();

                dbContext.Flights.AddOrUpdate(f => f.FlightCode, sofBer, berSof);

                dbContext.SaveChanges();
            }
        }
    }
}
