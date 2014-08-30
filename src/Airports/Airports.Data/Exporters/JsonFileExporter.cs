namespace Airports.Data.Exporters
{
    using System;
    using System.IO;
    using System.Linq;

    public static class JsonFileExporter
    {
        private const string ReportsFolderPath = @"..\..\..\..\Exports\Json-Reports\";
        private const string Extension = ".json";
        
        public static void GenerateReports()
        {
            // TODO: Dependency inversion
            Console.WriteLine("Exporting data to JSON reports...");

            CreateDirectoryIfNotExists(ReportsFolderPath);

            using (var db = new AirportsDbContext())
            {
                var allFlights = db.Flights.ToList();

                foreach (var flight in allFlights)
                {
                    using (var writer = new StreamWriter(ReportsFolderPath + flight.FlightId + Extension))
                    {
                        writer.WriteLine("{");
                        writer.WriteLine("\t\"flight-id\" : " + flight.FlightId);
                        writer.WriteLine("\t\"departure-airport-id\" : " + flight.DepartureAirportId);
                        writer.WriteLine("\t\"arrival-airport-id\" : " + flight.ArrivalAirportId);
                        writer.WriteLine("\t\"flight-code\" : " + flight.FlightCode);
                        writer.WriteLine("\t\"duration-hours\" : " + flight.DurationHours);
                        writer.WriteLine("\t\"airline-id\" : " + flight.AirlineId);
                        writer.WriteLine("}");
                    }
                }
            }

            // TODO: Handle possible exceptions
            Console.WriteLine("Done.");
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            // TODO: Handle possible exceptions
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}