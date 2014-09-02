namespace Airports.Client
{
    using Airports.Data;
    using Airports.Data.Exporters;
    using Airports.Data.Importers;
    using Airports.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Airports.Data.MongoDb;

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
            IAirportsData airportsData = new AirportsData();
            IAirportsMongoDbData mongoData = new AirportsMongoDbData();
            //mongoData.Insert(new Country() { Name = "Finland" });
            var bg = airportsData.Countries.SearchFor(x => x.Name == "Bulgaria").First();
            
            mongoData.Insert(bg);
            /*Task 1:
             * a) Extract *.xls and *.xlsx files from a zip archive; read and load the data into SQL Server.
             * b) Import data from MongoDb to SQL Server. */
            //ExtractZipAndImportDataFromExcelAndMongoDb(airportsData, mongoData);
            
            //Task 2: Generate PDF Reports
            //GeneratePdfFlightsReport(airportsData);

            //Tast 3: Generate report in XML format 
            //GenerateXmlFlightsReport(airportsData);

            // Task 4: a) Genetare JSON reports from SQL Server to file system.
            //         b) Import reports from file system (.json files) to MySQL.
            //GenerateJsonFlightsReportsAndLoadToMySql(airportsData);

            /* Task 5: Load Data from XML and save it in SQL Server and MongoDb */
            ImportFlightsDataFromXmlAndLoadToMongoDb(airportsData, mongoData);
        }

        private static void ExtractZipAndImportDataFromExcelAndMongoDb(IAirportsData airportsData, IAirportsMongoDbData mongoData)
        {
            Console.WriteLine("Unpacking zip archive...");
            var zipExtractor = new ZipExtractor();
            zipExtractor.Extract(SampleFlightsArchivedFilePath, SampleFlightsUnpackedDestinationPath);

            Console.WriteLine("Importing flights data from directory with xls/xlsx files...");
            var excelDataImporter = new ExcelDataImporter();
            ICollection<Flight> importedFlightsFromExcel = excelDataImporter
                .ImportFlightsDataFromDirectory(SampleFlightsUnpackedDestinationPath);

            Console.WriteLine("Loading imported flights from Excel to SQL Server...");

            foreach (var flight in importedFlightsFromExcel)
            {
                airportsData.Flights.Add(flight);
            }

            airportsData.SaveChanges();

            ICollection<Airline> importedAirlinesFromMongoDb = mongoData.Airlines.ToList();
            foreach (var airline in importedAirlinesFromMongoDb)
            {
                airportsData.Airlines.Add(airline);
            }
            airportsData.SaveChanges();

            ICollection<Country> importedCountriesFromMongoDb = mongoData.Countries.ToList();
            foreach (var country in importedCountriesFromMongoDb)
            {
                airportsData.Countries.Add(country);
            }
            airportsData.SaveChanges();

            ICollection<City> importedCitiesFromMongoDb = mongoData.Cities.ToList();
            foreach (var city in importedCitiesFromMongoDb)
            {
                airportsData.Cities.Add(city);
            }
            airportsData.SaveChanges();
        }

        private static void GeneratePdfFlightsReport(IAirportsData airportsData)
        {
            Console.WriteLine("Exporting PDF flight report...");
            var pdfExporter = new PdfFileExporter(PdfReportsFolderPath, PdfReportsFileName, airportsData);
            pdfExporter.GeneratePdfReport();
            Console.WriteLine("PDF flights report done!");
        }

        private static void GenerateXmlFlightsReport(IAirportsData airportsData)
        {
            Console.WriteLine("Exporting XML airlines report...");
            XmlFileExporter.GenerateAirlinesReport(airportsData);
            Console.WriteLine("XML airlines report done!");
        }

        private static void GenerateJsonFlightsReportsAndLoadToMySql(IAirportsData airportsData)
        {
            JsonFileExporter.GenerateReports(airportsData, JsonReportsFolderPath);
            //MySqlReportsImporter.ImportJsonReport(JsonReportsFolderPath);
        }

        private static void ImportFlightsDataFromXmlAndLoadToMongoDb(IAirportsData airportsData, IAirportsMongoDbData mongoData)
        {
            Console.WriteLine("Importing flights data from xml file...");
            XmlDataImporter xmlDataImporter = new XmlDataImporter();
            ICollection<Flight> importedFlightsFromXml = xmlDataImporter
                .ImportFlightsDataFromFile(SampleFlightsDataXmlFilePath);

            Console.WriteLine("Loading imported flights to SQL Server and MongoDb...");

            foreach (var flight in importedFlightsFromXml)
            {
                //airportsData.Flights.Add(flight);
                //mongoData.Insert(flight);
            }

            airportsData.SaveChanges();
        }
    }
}