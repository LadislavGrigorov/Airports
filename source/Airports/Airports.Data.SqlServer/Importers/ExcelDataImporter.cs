namespace Airports.Data.SqlServer.Importers
{
    using Airports.Data.SqlServer;
    using Airports.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ExcelDataImporter
    {
        private const string WorksheetFileExtensionPattern = @".xls[x]?\b";
        private const string FlightsWorksheetFilePattern = @"\w{3}-(Departures|Arrivals)-\d{2}-\w{3}-\d{4}.xls[x]?\b";
        private const string InvalidFileNameMessage = @"Provided file name is either invalid or does not match 
                                                    the naming convention for an xls/xlsx [{0}] data file.";

        public ICollection<Flight> ImportFlightsDataFromDirectory(string directoryPath)
        {
            IEnumerable<string> filePaths = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                                     .Where(p => Regex.IsMatch(p, FlightsWorksheetFilePattern));

            ICollection<Flight> importedFlights = new HashSet<Flight>();

            foreach (var path in filePaths)
            {
                foreach (var flight in this.ImportFlightsDataFromFile(path))
                {
                    importedFlights.Add(flight);
                }
            }

            return importedFlights;
        }

        public ICollection<Flight> ImportFlightsDataFromFile(string filePath)
        {
            if (!Regex.IsMatch(filePath, FlightsWorksheetFilePattern))
            {
                throw new ArgumentException(string.Format(InvalidFileNameMessage, "Flights"));
            }

            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = string.Format(AirportsDbSettings.Default.ExcelReaderConnectionString, filePath);

            connection.Open();

            using (connection)
            {
                var schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var sheetName = schema.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand selectAllRowsCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "]", connection);

                ICollection<Flight> importedFlights = new HashSet<Flight>();

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectAllRowsCommand))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    using (DataTableReader reader = dataSet.CreateDataReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                string flightCode = reader["FlightCode"].ToString();
                                int airlineId = int.Parse(reader["AirlineId"].ToString());
                                int departureAirportId = int.Parse(reader["DepartureAirportId"].ToString());
                                int arrivalAirportId = int.Parse(reader["ArrivalAirportId"].ToString());
                                double durationHours = double.Parse(reader["Duration"].ToString());
                                DateTime date = DateTime.Parse(reader["DateTime"].ToString(), CultureInfo.InvariantCulture);
                                
                                var flight = new Flight()
                                {
                                    FlightCode = flightCode,
                                    AirlineId = airlineId,
                                    FlightDate = date,
                                    DurationHours = durationHours,
                                    DepartureAirportId = departureAirportId,
                                    ArrivalAirportId = arrivalAirportId
                                };

                                importedFlights.Add(flight);
                            }
                            catch (FormatException)
                            { }
                        }
                    }
                }

                return importedFlights;
            }
        }
    }
}