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
            CreateDirectoryIfNotExists(reportsFolderPath);

            var allFlights = context.Flights.GetAll().ToList();

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