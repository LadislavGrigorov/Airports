namespace Airports.Data.Exporters
{
    using System;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public static class PdfFileExporter
    {
        private const string ReportsFolderPath = @"..\..\..\..\Exports\PDF-Reports\";

        public static void GeneratePdfReport()
        {
            CreateDirectoryIfNotExists(ReportsFolderPath);
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new FileStream(@"..\..\..\..\Exports\PDF-Reports\flight-report.pdf", FileMode.Create, FileAccess.Write);
            var writer = PdfWriter.GetInstance(document, output);

            PdfPTable table = CreateTable();
            AddTableHeader(table);
            AddTableColumns(table);
            FillTableData(table);

            document.Open();
            document.Add(table);
            document.Close();
        }

        private static void FillTableData(PdfPTable table)
        {
            var airportsData = new AirportsData();

            var flights = airportsData.Flights.GetAll().ToList();

            foreach (var flight in flights)
            {
                table.AddCell(flight.FlightId.ToString());
                table.AddCell(flight.DepartureAirport.Name + " (" + flight.DepartureAirport.AirportCode + ")");
                table.AddCell(flight.ArrivalAirport.Name + " (" + flight.ArrivalAirport.AirportCode + ")");
                table.AddCell(flight.FlightCode.ToString());
                table.AddCell(flight.FlightDate.ToString("yyyy-MM-dd hh:mm:ss"));
                table.AddCell(flight.DurationHours.ToString() + " hours");
                table.AddCell(flight.Airline.Name);
            }
        }

        private static void AddTableColumns(PdfPTable table)
        {
            table.AddCell("Flight");
            table.AddCell("Departure Airport");
            table.AddCell("Arrival Airport");
            table.AddCell("Code");
            table.AddCell("Date");
            table.AddCell("Duration");
            table.AddCell("Airline");
        }

        private static void AddTableHeader(PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Phrase("Flights"));
            cell.Colspan = 7;
            cell.HorizontalAlignment = 1;
            cell.BackgroundColor = BaseColor.GRAY;
            table.AddCell(cell);
        }

        private static PdfPTable CreateTable()
        {
            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            table.LockedWidth = false;
            float[] widths = new float[] { 2f, 3f, 3f, 3f, 3f, 2f, 2f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            return table;
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
