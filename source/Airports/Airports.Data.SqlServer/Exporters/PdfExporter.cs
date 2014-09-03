namespace Airports.SqlServer.Data.Exporters
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using Airports.Data.SqlServer;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfFileExporter
    {
        private const string FlightIdColumnHeader = "Airline";
        private const string DepartureAirportColumnHeader = "Total Flights";
        private const string ArrivalAirportColumnHeader = "Average Flight Duration";
        private const string FlightCodeColumnHeader = "Total Flight Duration";

        public void GenerateAggregatedAirlineReports(string filePath, string fileName, IAirportsDataSqlServer airportsData)
        {
            fileName = AddUniqueFilenameSuffix(fileName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new FileStream(filePath + fileName, FileMode.Create, FileAccess.Write);
            var writer = PdfWriter.GetInstance(document, output);

            PdfPTable table = this.CreateAirlineReportsTable();
            this.AddAirlineReportsTableHeader(table);
            this.AddAirlineReportsTableColumns(table);
            this.FillAirlineReportsTableData(table, airportsData);

            document.Open();
            document.Add(table);
            document.Close();
        }

        private void FillAirlineReportsTableData(PdfPTable table, IAirportsDataSqlServer airportsData)
        {
            var aggregatedAirlineReports = airportsData.Airlines.GetAll()
                .Include("Flights")
                .Select(a =>
                    new
                    {
                        a.Name,
                        TotalFlightsCount = a.Flights.Count,
                        AverageFlightDuration = a.Flights.Average(f => f.DurationHours),
                        TotalFlightDuration = a.Flights.Sum(f => f.DurationHours)
                    })
                .ToList();

            foreach (var airlineReport in aggregatedAirlineReports)
            {
                table.AddCell(airlineReport.Name);
                table.AddCell(airlineReport.TotalFlightsCount.ToString());
                table.AddCell(airlineReport.AverageFlightDuration.ToString() + " hours");
                table.AddCell(airlineReport.TotalFlightDuration.ToString());
            }
        }

        private void AddAirlineReportsTableColumns(PdfPTable table)
        {
            table.AddCell(FlightIdColumnHeader);
            table.AddCell(DepartureAirportColumnHeader);
            table.AddCell(ArrivalAirportColumnHeader);
            table.AddCell(FlightCodeColumnHeader);
        }

        private void AddAirlineReportsTableHeader(PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Phrase("Airline Reports"));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            cell.BackgroundColor = BaseColor.GRAY;
            table.AddCell(cell);
        }

        private PdfPTable CreateAirlineReportsTable()
        {
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.LockedWidth = false;
            float[] widths = new float[] { 3f, 3f, 3f, 3f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            return table;
        }

        private static string AddUniqueFilenameSuffix(string fileName)
        {
            DateTime now = DateTime.Now;
            string fileNameSuffix = string.Format("-{0}.{1}.{2}-{3}.{4}.{5}.pdf",
                now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);

            fileName = fileName + fileNameSuffix;
            return fileName;
        }
    }
}
