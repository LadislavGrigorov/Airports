namespace Airports.Client
{
    using System;
    using System.Linq;
    using Airports.Data;
    using Airports.Data.Migrations;
    using Airports.Models;
    using System.Data.Entity;

    class Program
    {
        static void Main()
        {
            //Delete or comment after db initialization
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AirportsDbContext, Configuration>());

            DbContentConsolePrinter.PrintCountries();
            DbContentConsolePrinter.PrintCities();
            DbContentConsolePrinter.PrintDbContent();
        }

    }
}
