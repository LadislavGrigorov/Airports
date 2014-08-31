namespace Airports.Client
{
    using Airports.Data;
    using Airports.Data.Exporters;
    using Airports.Data.Importers;
    using System;

    class Program
    {
        private const string SampleFlightsArchivedFilePath = @"..\..\..\..\Imports\Sample-Flights.zip";
        private const string SampleFlightsUnpackedDestinationPath = @"..\..\..\..\Imports\Sample-Flights-Unpacked\";

        static void Main()
        {
            /*Task 1:
             * a) Extract *.xls and *.xlsx files from a zip archive; read and load the data into SQL Server.
             * b) Import data MongoDb into SQL Server. */

            var airportsData = new AirportsData();
            
            var zipExtractor = new ZipExtractor();
            Console.WriteLine("Unpacking zip archive...");
            zipExtractor.Extract(SampleFlightsArchivedFilePath, SampleFlightsUnpackedDestinationPath);

            var excelDataImporter = new ExcelDataImporter();
            Console.WriteLine("Importing xls flight data from directory...");
            var importedFlights = excelDataImporter.ImportFlightsDataFromDirectory(SampleFlightsUnpackedDestinationPath);

            Console.WriteLine("Loading imported flights to SQL Server...");
            foreach (var flight in importedFlights)
            {
                airportsData.Flights.Add(flight);
            }

            Console.WriteLine("Done.");

            //Task 2: Generate PDF Reports
            Console.WriteLine("Exporting PDF flights report....");
            PdfFileExporter.GeneratePdfReport();
            Console.WriteLine("PDF flights report done!");
            
            // Task 4: a) Genetare JSON reports from SQL Server to file system.
            JsonFileExporter.GenerateReports();
        }
    }
}

