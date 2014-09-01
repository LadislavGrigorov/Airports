namespace Airports.Data.Exporters
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;

    public static class JsonFileExporter
    {
        private const string Extension = ".json";
        
        public static void GenerateReports(IAirportsData context, string reportsFolderPath)
        {
            Console.WriteLine("Exporting data to JSON reports...");

            CreateDirectoryIfNotExists(reportsFolderPath);

            var allFlights = context.Flights.GetAll().ToList();

            foreach (var flight in allFlights)
            {
                string json = JsonConvert.SerializeObject(flight, Formatting.Indented);

                using (var writer = new StreamWriter(reportsFolderPath + flight.FlightId + Extension))
                {
                    writer.Write(json);
                    //writer.WriteLine("{");
                    //writer.WriteLine("\t\"flight-id\" : " + flight.FlightId);
                    //writer.WriteLine("\t\"departure-airport-id\" : " + flight.DepartureAirportId);
                    //writer.WriteLine("\t\"arrival-airport-id\" : " + flight.ArrivalAirportId);
                    //writer.WriteLine("\t\"flight-code\" : " + flight.FlightCode);
                    //writer.WriteLine("\t\"duration-hours\" : " + flight.DurationHours);
                    //writer.WriteLine("\t\"airline-id\" : " + flight.AirlineId);
                    //writer.WriteLine("}");
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