namespace Airports.SqlServer.Data.Exporters
{
    using System.IO;
    using System.Linq;
    using Airports.Data.SqlServer;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfFileExporter
    {
        private const string FlightIdColumnHeader = "Flight";
        private const string DepartureAirportColumnHeader = "Departure Airport";
        private const string ArrivalAirportColumnHeader = "Arrival Airport";
        private const string FlightCodeColumnHeader = "Code";
        private const string FlightDateColumnHeader = "Date";
        private const string FlightDurationColumnHeader = "Duration";
        private const string FlightAirlineColumnHeader = "Airline";

        public void GeneratePdfReport(string filePath, string fileName, IAirportsDataSqlServer airportsData)
        {
            this.CreateDirectoryIfNotExists(filePath);
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new FileStream(filePath + fileName, FileMode.Create, FileAccess.Write);
            var writer = PdfWriter.GetInstance(document, output);

            PdfPTable table = this.CreateTable();
            this.AddTableHeader(table);
            this.AddTableColumns(table);
            this.FillTableData(table, airportsData);

            document.Open();
            document.Add(table);
            document.Close();
        }

        private void FillTableData(PdfPTable table, IAirportsDataSqlServer airportsData)
        {
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

        private void AddTableColumns(PdfPTable table)
        {
            table.AddCell(FlightIdColumnHeader);
            table.AddCell(DepartureAirportColumnHeader);
            table.AddCell(ArrivalAirportColumnHeader);
            table.AddCell(FlightCodeColumnHeader);
            table.AddCell(FlightDateColumnHeader);
            table.AddCell(FlightDurationColumnHeader);
            table.AddCell(FlightAirlineColumnHeader);
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
