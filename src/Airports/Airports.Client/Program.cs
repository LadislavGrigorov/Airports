namespace Airports.Client
{
    using System;
    using System.Linq;
    using Airports.Data;
    using Airports.Data.Migrations;
    using Airports.Data.Exporters;
    using Airports.Models;
    using System.Data.Entity;
    using Airports.Data.Importers;

    class Program
    {
        private const string SampleFlightsArchivedFilePath = @"..\..\..\..\Imports\Sample-Flights.zip";
        private const string SampleFlightsUnpackedFilePath = @"..\..\..\..\Imports\Sample-Flights-Extracted";

        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AirportsDbContext, Configuration>());

            /*Task 1:
             * a) Extract *.xls and *.xlsx files from a zip archive; read and load the data into SQL Server.
             * b) Import data MongoDb into SQL Server. */
            var zipExtractor = new ZipExtractor();
            zipExtractor.Extract(SampleFlightsArchivedFilePath, SampleFlightsUnpackedFilePath);
        }

    }
}
