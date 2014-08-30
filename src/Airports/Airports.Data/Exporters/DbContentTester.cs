namespace Airports.Data.Exporters
{
    using Airports.Data;
    using System;
    using System.Linq;

    /// <summary>
    /// This class is used only for testing purposes, and should be deleted afterwards.
    /// </summary>
    public static class DbContentTester
    {
        private static readonly string DecorationLine = new string('-', 25);

        public static void PrintCountriesAndTheirCities()
        {
            var airportsData = new AirportsData();

            var countries = airportsData.Countries.GetAll().ToList();

            foreach (var country in countries)
            {
                Console.WriteLine(country.Name);

                var cities = country.Cities.ToList();

                foreach (var city in cities)
                {
                    Console.WriteLine(city.Name);
                }
            }

            Console.WriteLine(DecorationLine);
        }

        public static void PrintAllFlightsData()
        {
            var airportsData = new AirportsData();
            var flights = airportsData.Flights.GetAll().ToList();

            foreach (var flight in flights)
            {
                Console.WriteLine("{0}:, {1}, {2} hours, {3} -> {4}, ({5})", 
                    flight.FlightCode, 
                    flight.FlightDate, 
                    flight.DurationHours, 
                    flight.DepartureAirport.Name, 
                    flight.ArrivalAirport.Name, 
                    flight.Airline.Name);
            }
        }
    }
}
