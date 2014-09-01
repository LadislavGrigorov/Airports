namespace Airports.Client
{
    using Airports.Data;
    using Airports.Data.Exporters;
    using Airports.Data.Importers;
    using Airports.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private const string SampleFlightsArchivedFilePath = @"..\..\..\..\Imports\Sample-Flights.zip";
        private const string SampleFlightsUnpackedDestinationPath = @"..\..\..\..\Imports\Sample-Flights-Unpacked\";
        private const string SampleFlightsDataXmlFilePath = @"..\..\..\..\Imports\CDG-Departures-01-Sep-2014.xml";

        private const string JsonReportsFolderPath = @"..\..\..\..\Exports\Json-Reports\";

        private const string PdfReportsFolderPath = @"..\..\..\..\Exports\PDF-Reports\";
        private const string PdfReportsFileName = @"flight-report.pdf";

        static void Main()
        {
            /*Task 1:
             * a) Extract *.xls and *.xlsx files from a zip archive; read and load the data into SQL Server.
             * b) Import data from MongoDb to SQL Server. */

            var airportsData = new AirportsData();

            Console.WriteLine("Unpacking zip archive...");
            var zipExtractor = new ZipExtractor();
            zipExtractor.Extract(SampleFlightsArchivedFilePath, SampleFlightsUnpackedDestinationPath);

            Console.WriteLine("Importing flights data from directory with xls/xlsx files...");
            var excelDataImporter = new ExcelDataImporter();
            ICollection<Flight> importedFlights = excelDataImporter
                .ImportFlightsDataFromDirectory(SampleFlightsUnpackedDestinationPath);

            Console.WriteLine("Loading imported flights to SQL Server...");
            foreach (var flight in importedFlights)
            {
                airportsData.Flights.Add(flight);
            }
            airportsData.SaveChanges();

            //Task 2: Generate PDF Reports
            Console.WriteLine("Exporting PDF flights report...");
            var pdfExporter = new PdfFileExporter(PdfReportsFolderPath, PdfReportsFileName, airportsData);
            pdfExporter.GeneratePdfReport();
            Console.WriteLine("PDF flights report done!");

            //Tast 3: Generate report in XML format 
            Console.WriteLine("Exporting XML airlines report...");
            XmlFileExporter.GenerateAirlinesReport(airportsData);
            Console.WriteLine("XML airlines report done!");

            // Task 4: a) Genetare JSON reports from SQL Server to file system.
            JsonFileExporter.GenerateReports(airportsData, JsonReportsFolderPath);

            /* Task 5: Load Data from XML and save it in SQL Server and MongoDb */
            Console.WriteLine("Importing flights data from xml file...");
            XmlDataImporter xmlDataImporter = new XmlDataImporter();
            ICollection<Flight> importedFlightsFromXml = xmlDataImporter
                .ImportFlightsDataFromFile(SampleFlightsDataXmlFilePath);

            Console.WriteLine("Loading imported flights to SQL Server...");
            foreach (var flight in importedFlightsFromXml)
            {
                airportsData.Flights.Add(flight);
            }
            airportsData.SaveChanges();
        }
    }
}