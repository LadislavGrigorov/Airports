namespace Airports.Data.Reporters
{
    using Airports.Data;
    using System;
    using System.Linq;

    /// <summary>
    /// This class is used only for testing purposes, and should be deleted afterwards.
    /// </summary>
    public static class DbContentConsolePrinter
    {
        private static readonly string DecorationLine = new string('-', 25);

        public static void PrintCountries()
        {
            using (AirportsDbContext dbContext = new AirportsDbContext())
            {
                Console.WriteLine(DecorationLine);

                var countries = dbContext.Countries.ToArray();
                Console.WriteLine("Countries:");

                foreach (var country in countries)
                {
                    Console.WriteLine("{0}", country.Name);
                }
                Console.WriteLine(DecorationLine);
            }
        }

        public static void PrintCities()
        {
            using (AirportsDbContext dbContext = new AirportsDbContext())
            {
                Console.WriteLine(DecorationLine);
                var cities = dbContext.Cities.ToArray();
                Console.WriteLine("Cities:");

                foreach (var city in cities)
                {
                    Console.WriteLine("{0}, {1}", city.Name, city.Country.Name);
                }
                Console.WriteLine(DecorationLine);
            }
        }

        public static void PrintDbContent()
        {
            using (AirportsDbContext dbContext = new AirportsDbContext())
            {
                Console.WriteLine(DecorationLine);
                var airlines = dbContext.Airlines.ToList();
                Console.WriteLine("Airlines:");

                foreach (var airline in airlines)
                {
                    Console.WriteLine(airline.Name);
                    var airlineFlights = airline.Flights.ToList();

                    foreach (var flight in airlineFlights)
                    {
                        Console.WriteLine("{0},{1}->{2} [{3}]",
                                    flight.FlightCode, flight.DepartureAirport.Name, flight.ArrivalAirport.Name, flight.Airline.Name);
                    }
                }

                var airports = dbContext.Airports.ToList();
                Console.WriteLine("Airports:");

                foreach (var airport in airports)
                {
                    Console.WriteLine("[{0}], {1}, {2}", airport.AirportCode, airport.Name, airport.City.Name); 

                    var departures = airport.DepartureFlights.ToList();
                    Console.WriteLine("Departures:");

                    foreach (var flight in departures)
                    {
                        Console.WriteLine("{0}: {1} -> {2}",
                            flight.FlightCode, flight.DepartureAirport.Name, flight.ArrivalAirport.Name);
                    }

                    var arrivals = airport.ArrivalFlights.ToList();
                    Console.WriteLine("Arrivals:");

                    foreach (var flight in arrivals)
                    {
                        Console.WriteLine("{0}: {1} -> {2}",
                            flight.FlightCode, flight.DepartureAirport.Name, flight.ArrivalAirport.Name);
                    }
                }
                Console.WriteLine(DecorationLine);
            }
        }
    }
}
