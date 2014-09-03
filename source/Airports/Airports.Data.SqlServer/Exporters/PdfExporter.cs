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
        private const string AirlineNameColumnHeader = "Airline";
        private const string TotalFlightsColumnHeader = "Total Flights";
        private const string AverageFlightDurationColumnHeader = "Average Flight Duration";
        private const string TotalFlightDurationColumnHeader = "Total Flight Duration";
        private const int PdfTableSize = 4;

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
            table.AddCell(AirlineNameColumnHeader);
            table.AddCell(TotalFlightsColumnHeader);
            table.AddCell(AverageFlightDurationColumnHeader);
            table.AddCell(TotalFlightDurationColumnHeader);
        }

        private void AddAirlineReportsTableHeader(PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Phrase("Airline Reports"));
            cell.Colspan = PdfTableSize;
            cell.HorizontalAlignment = 1;
            cell.BackgroundColor = BaseColor.GRAY;
            table.AddCell(cell);
        }

        private PdfPTable CreateAirlineReportsTable()
        {
            PdfPTable table = new PdfPTable(PdfTableSize);
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
            DateTime currentDate = DateTime.Now;
            string fileNameSuffix = string.Format("-{0}.{1}.{2}-{3}.{4}.{5}.pdf",
                currentDate.Day, currentDate.Month, currentDate.Year, currentDate.Hour, currentDate.Minute, currentDate.Second);

            fileName = fileName + fileNameSuffix;
            return fileName;
        }
    }
}
