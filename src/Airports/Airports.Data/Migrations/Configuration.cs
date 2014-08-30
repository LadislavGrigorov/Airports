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
                var bulgaria = new Country() { CountryId = 1, Name = "Bulgaria" };
                var france = new Country() { CountryId = 2, Name = "France" };
                var germany = new Country() { CountryId = 3, Name = "Germany" };
                var spain = new Country() { CountryId = 4, Name = "Spain" };
                var uk = new Country() { CountryId = 5, Name = "United Kingdom" };

                dbContext.Countries.AddOrUpdate(c => c.Name, bulgaria, france, germany, spain, uk);
                dbContext.SaveChanges();

                var sofia = new City() { CityId = 1, Name="Sofia", CountryId = 1 };
                var paris = new City() { CityId = 2, Name="Paris", CountryId = 2 };
                var berlin = new City() { CityId = 3, Name = "Berlin",CountryId = 3 };
                var barcelona = new City() { CityId = 4,Name="Barcelona", CountryId = 4 };
                var london = new City() { CityId = 5,Name="London", CountryId = 5 };

                dbContext.Cities.AddOrUpdate(c => c.Name, sofia, paris, berlin, barcelona, london);
                dbContext.SaveChanges();

                var airportSofia = new Airport() { AirportId = 1, AirportCode = "SOF", CityId = 1, Name = "Sofia Airport" };
                var airportParis = new Airport() { AirportId = 2, AirportCode = "CDG", CityId = 2, Name = "Charles de Gaulle Airport" };
                var airportBerlin = new Airport() { AirportId = 3, AirportCode = "TXL", CityId = 3, Name = "Berlin Tegel" };
                var airportBarcelona = new Airport() { AirportId = 4, AirportCode = "BCN", CityId = 4, Name="Barcelona El-Prat Airport"};
                var airportLondon = new Airport() { AirportId = 5, AirportCode = "LHR", CityId = 5, Name = "London Heathrow" };

                dbContext.Airports.AddOrUpdate(a => a.AirportCode, 
                    airportSofia, airportParis, airportBerlin, airportBarcelona, airportLondon);
                dbContext.SaveChanges();

                var airlineBulgariaAir = new Airline() { AirlineId = 1, Name = "Bulgaria Air" };
                var airlineAirFrance = new Airline() { AirlineId = 2, Name = "Air France" };
                var airlineLufhansa = new Airline() { AirlineId = 3, Name = "Lufthansa" };
                var airlineIberia = new Airline() { AirlineId = 4, Name = "Iberia" };
                var airlineBritishAirways = new Airline() { AirlineId = 5, Name = "British Airways" };

                dbContext.Airlines.AddOrUpdate(a => a.Name, 
                    airlineBulgariaAir, airlineLufhansa, airlineAirFrance, airlineIberia, airlineBritishAirways);
                dbContext.SaveChanges();

                var flightSofiaParis = new Flight()
                {
                    AirlineId = 1,
                    DepartureAirportId = 1,
                    ArrivalAirportId = 2,
                    DurationHours = 2.5,
                    FlightCode = "9OTT12",
                    FlightDate = DateTime.Now,
                };

                dbContext.Flights.AddOrUpdate(f => f.FlightCode, flightSofiaParis);
                dbContext.SaveChanges();

                var flightParisBerlin = new Flight()
                {
                    AirlineId = 2,
                    DepartureAirportId = 2,
                    ArrivalAirportId = 3,
                    DurationHours = 1.0,
                    FlightCode = "UT66PB",
                    FlightDate = DateTime.Now.AddHours(2),
                };

                dbContext.Flights.AddOrUpdate(f => f.FlightCode, flightParisBerlin);
                dbContext.SaveChanges();

                var flightBerlinSofia = new Flight()
                {
                    AirlineId = 3,
                    DepartureAirportId = 3,
                    ArrivalAirportId = 1,
                    DurationHours = 3.0,
                    FlightCode = "AA99Y2",
                    FlightDate = DateTime.Now.AddHours(5),
                };

                dbContext.Flights.AddOrUpdate(f => f.FlightCode, flightBerlinSofia);
                dbContext.SaveChanges();
            }
        }
    }
}
