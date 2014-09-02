namespace Airports.Data.Exporters
{
    using System;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfFileExporter
    { 
        private const string FirstColumnName = "Flight";
        private const string SecondColumnName = "Departure Airport";
        private const string ThirdColumnName = "Arrival Airport";
        private const string FourthColumnName = "Code";
        private const string FifthColumnName = "Date";
        private const string SixthColumnName = "Duration";
        private const string SeventhColumnName = "Airline";

        private readonly string filePath;
        private readonly string fileName;
        private readonly IAirportsData airportsData;

        public PdfFileExporter(string filePath, string fileName, IAirportsData airportsData)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.airportsData = airportsData;
        }

        public void GeneratePdfReport()
        {
            this.CreateDirectoryIfNotExists(this.filePath);
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new FileStream(this.filePath + this.fileName, FileMode.Create, FileAccess.Write);
            var writer = PdfWriter.GetInstance(document, output);

            PdfPTable table = this.CreateTable();
            this.AddTableHeader(table);
            this.AddTableColumns(table);
            this.FillTableData(table);

            document.Open();
            document.Add(table);
            document.Close();
        }

        private void FillTableData(PdfPTable table)
        {
            var flights = this.airportsData.Flights.GetAll().ToList();
            
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

        private void AddTableColumns(PdfPTable table)
        {
            table.AddCell(FirstColumnName);
            table.AddCell(SecondColumnName);
            table.AddCell(ThirdColumnName);
            table.AddCell(FourthColumnName);
            table.AddCell(FifthColumnName);
            table.AddCell(SixthColumnName);
            table.AddCell(SeventhColumnName);
        }

        private void AddTableHeader(PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Phrase("Flights"));
            cell.Colspan = 7;
            cell.HorizontalAlignment = 1;
            cell.BackgroundColor = BaseColor.GRAY;
            table.AddCell(cell);
        }

        private PdfPTable CreateTable()
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

        private void CreateDirectoryIfNotExists(string path)
        {
            // TODO: Handle possible exceptions
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
