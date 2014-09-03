namespace Airports.Data.SqlServer.Exporters
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;
    using System.Data.Entity;
using System;

    public class JsonFileExporter
    {
        private const string Extension = ".json";
        private DateTime DefaultStartDate = DateTime.MinValue;
        private DateTime DefaultEndDate = DateTime.MaxValue;
        
        public void GenerateReports(
            IAirportsDataSqlServer context, 
            string reportsFolderPath, 
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            startDate = startDate ?? new DateTime(1990, 1, 1);
            endDate = endDate ?? new DateTime(2015, 1, 1);

            CreateDirectoryIfNotExists(reportsFolderPath);

            var aggregatedAirlineReports = context.Airlines.GetAll()
                .Include(a => a.Flights)
                .Select(a =>
                    new
                    {
                        a.AirlineId,
                        a.Name,
                        Flights = a.Flights.Where(f => startDate < f.FlightDate && f.FlightDate < endDate)
                    })
                .Select(a =>
                    new
                    {
                        AirlineId = a.AirlineId,
                        AirlineName = a.Name,
                        TotalFlightsCount = a.Flights.Count(),
                        AverageFlightsCount = a.Flights.Average(f => f.DurationHours), //AverageFligtsDuration
                        TotalFlightsDuration = a.Flights.Sum(f => f.DurationHours),                        
                        StartDate = startDate,
                        EndDate = endDate
                    })
                .ToList();

            foreach (var flight in aggregatedAirlineReports)
            {
                string json = JsonConvert.SerializeObject(flight, Formatting.Indented);

                using (var writer = new StreamWriter(reportsFolderPath + flight.AirlineId + Extension))
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