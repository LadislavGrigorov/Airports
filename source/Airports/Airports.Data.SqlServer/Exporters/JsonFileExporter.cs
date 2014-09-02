namespace Airports.Data.SqlServer.Exporters
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;
    using System.Data.Entity;

    public static class JsonFileExporter
    {
        private const string Extension = ".json";
        
        public static void GenerateReports(IAirportsDataSqlServer context, string reportsFolderPath)
        {
            CreateDirectoryIfNotExists(reportsFolderPath);

            var allFlights = context.Flights.GetAll()
               .Include("DepartureAirport")
               .Include("ArrivalAirport")
               .Include("Airline")
               .ToList();

            foreach (var flight in allFlights)
            {
                string json = JsonConvert.SerializeObject(flight, Formatting.Indented);

                using (var writer = new StreamWriter(reportsFolderPath + flight.FlightId + Extension))
                {
                    writer.Write(json);
                }
            }

            // TODO: Handle possible exceptions
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