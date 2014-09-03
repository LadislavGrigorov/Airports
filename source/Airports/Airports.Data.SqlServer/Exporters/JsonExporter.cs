namespace Airports.Data.SqlServer.Exporters
{
    using Newtonsoft.Json;
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;

    public class JsonExporter
    {
        private const string FileExtension = ".json";
        
        public void GenerateReportsForGivenPeriod(
            IAirportsDataSqlServer context, 
            string reportsFolderPath, 
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            if (!Directory.Exists(reportsFolderPath))
            {
                Directory.CreateDirectory(reportsFolderPath);
            }

            startDate = startDate ?? new DateTime(1900, 1, 1);
            endDate = endDate ?? DateTime.Now;

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
                        AverageFlightsCount = a.Flights.Average(f => f.DurationHours), 
                        TotalFlightsDuration = a.Flights.Sum(f => f.DurationHours),                        
                        StartDate = startDate,
                        EndDate = endDate
                    })
                .ToList();

            foreach (var report in aggregatedAirlineReports)
            {
                string json = JsonConvert.SerializeObject(report, Formatting.Indented);

                using (var writer = new StreamWriter(reportsFolderPath + report.AirlineId + FileExtension))
                {
                    writer.Write(json);
                }
            }
        }
    }
}