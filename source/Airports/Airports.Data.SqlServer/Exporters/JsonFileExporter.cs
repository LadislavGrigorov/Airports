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
            startDate = startDate ?? DefaultStartDate;
            endDate = endDate ?? DefaultEndDate;

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
                        a.AirlineId,
                        a.Name,
                        FlightsCount = a.Flights.Count(),
                        TotalDuration = a.Flights.Sum(f => f.DurationHours),
                        AverageDuration = a.Flights.Average(f => f.DurationHours),
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