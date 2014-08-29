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
                foreach (var item in airports)
                {
                    Console.WriteLine(item.Name);
                }

                var sofAirport = dbContext.Airports.First(a => a.AirportCode == "SOF");
                var berAirport = dbContext.Airports.First(a => a.AirportCode == "BER");

                var sofDeps = sofAirport.DepartureFlights.ToArray();
                Console.WriteLine("SOF Deps:");
                foreach (var item in sofDeps)
                {
                    Console.WriteLine("{0}: {1} -> {2}", item.FlightCode, item.DepartureAirport.Name, item.ArrivalAirport.Name);
                }

                var sofArrs = sofAirport.ArrivalFlights.ToArray();
                Console.WriteLine("SOF Arrs:");
                foreach (var item in sofArrs)
                {
                    Console.WriteLine("{0}: {1} -> {2}", item.FlightCode, item.DepartureAirport.Name, item.ArrivalAirport.Name);
                }

                var berDeps = berAirport.DepartureFlights.ToArray();
                Console.WriteLine("BER Deps:");
                foreach (var item in berDeps)
                {
                    Console.WriteLine("{0}: {1} -> {2}", item.FlightCode, item.DepartureAirport.Name, item.ArrivalAirport.Name);
                }

                var berArrs = berAirport.ArrivalFlights.ToArray();
                Console.WriteLine("BER Arrs:");
                foreach (var item in berArrs)
                {
                    Console.WriteLine("{0}: {1} -> {2}", item.FlightCode, item.DepartureAirport.Name, item.ArrivalAirport.Name);
                }

                var flights = dbContext.Flights.ToList();
                Console.WriteLine("Flights:");
                foreach (var flight in flights)
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
