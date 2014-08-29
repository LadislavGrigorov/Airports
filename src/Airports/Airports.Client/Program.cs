namespace Airports.Client
{
    using System;
    using System.Linq;
    using Airports.Data;
    using Airports.Data.Migrations;
    using Airports.Models;
    using System.Data.Entity;

    class Program
    {
        static void Main()
        {
            //Delete or comment after db initialization
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AirportsDbContext, Configuration>());

            PrintDbContent();
        }

        /// <summary>
        /// Written for testing purposes.
        /// </summary>
        static void PrintDbContent()
        {
            using (AirportsDbContext dbContext = new AirportsDbContext())
            {
                var airlines = dbContext.Airlines.ToList();
                Console.WriteLine("Airlines:");
                foreach (var item in airlines)
                {
                    Console.WriteLine(item.Name);
                    var airlineFlights = item.Flights.ToList();

                    foreach (var flight in airlineFlights)
                    {
                        Console.WriteLine(flight.FlightCode);
                    }
                }

                var airports = dbContext.Airports.ToList();
                Console.WriteLine("Airports:");
                foreach (var airport in airports)
                {
                    Console.WriteLine(airport.Name);
                    Console.WriteLine(airport.City.Name);
                }

                var sofAirport = dbContext.Airports.First(a => a.AirportCode == "SOF");
                var berAirport = dbContext.Airports.First(a => a.AirportCode == "BER");

                var sofiaDepartues = sofAirport.DepartureFlights.ToArray();
                Console.WriteLine("SOF Departures:");
                foreach (var flight in sofiaDepartues)
                {
                    Console.WriteLine("{0}: {1} -> {2}", flight.FlightCode, flight.DepartureAirport.Name, flight.ArrivalAirport.Name);
                }

                var sofiaArrivals = sofAirport.ArrivalFlights.ToArray();
                Console.WriteLine("SOF Arrivals:");
                foreach (var flight in sofiaArrivals)
                {
                    Console.WriteLine("{0}: {1} -> {2}", flight.FlightCode, flight.DepartureAirport.Name, flight.ArrivalAirport.Name);
                }

                var berlinDepartures = berAirport.DepartureFlights.ToArray();
                Console.WriteLine("BER Departures:");
                foreach (var flight in berlinDepartures)
                {
                    Console.WriteLine("{0}: {1} -> {2}", flight.FlightCode, flight.DepartureAirport.Name, flight.ArrivalAirport.Name);
                }

                var berlinArrivals = berAirport.ArrivalFlights.ToArray();
                Console.WriteLine("BER Arrivalss:");
                foreach (var item in berlinArrivals)
                {
                    Console.WriteLine("{0}: {1} -> {2}", item.FlightCode, item.DepartureAirport.Name, item.ArrivalAirport.Name);
                }

                var allFlights = dbContext.Flights.ToList();
                Console.WriteLine("Flights:");
                foreach (var flight in allFlights)
                {
                    Console.WriteLine("{0},{1}->{2} [{3}]", 
                        flight.FlightCode, 
                        flight.DepartureAirport.Name, 
                        flight.ArrivalAirport.Name, 
                        flight.Airline.Name);
                }
            }
        }
    }
}
