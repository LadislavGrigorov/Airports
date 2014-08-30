namespace Airports.Client
{
    using System;
    using System.Linq;
    using Airports.Data;
    using Airports.Data.Migrations;
    using Airports.Data.Exporters;
    using Airports.Models;
    using System.Data.Entity;

    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AirportsDbContext, Configuration>());

            DbContentConsolePrinter.PrintCountries();
            DbContentConsolePrinter.PrintCities();
            DbContentConsolePrinter.PrintDbContent();
        }

    }
}
