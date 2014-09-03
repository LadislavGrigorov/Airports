namespace Airports.Data.MySql.Exporters
{
    using System;
    using System.IO;
    using System.Linq;
    using Airports.Models.MySql;
    using SpreadsheetLight;
    using Airports.Data.SQLite;

    public static class ExcelExporter
    {
        public static void Test()
        {
            AirportsDbContextSQLite sqliteDbContext = new AirportsDbContextSQLite();
            using (sqliteDbContext)
            {
                var all = sqliteDbContext.Airlines;

                foreach (var item in all)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
        //GenerateCompositeReport
        public static void GenerateExcelFile(string path)
        {
            Console.WriteLine("Generating merged report from SQLite and MySql...");

            CreateDirectoryIfNotExists(path);

            SLDocument excelFile = new SLDocument();

            using (AirportsDbContextMySql dbContext = new AirportsDbContextMySql())
            {
                var allData = dbContext.Airlinereports.ToList();

                int rowCounter = 1;
                foreach (var row in allData)
                {
                    excelFile.SetCellValue("A" + rowCounter, row.AirlineId.ToString());
                    excelFile.SetCellValue("B" + rowCounter, row.AirlineName);
                    excelFile.SetCellValue("C" + rowCounter, row.AverageFlightsCount.ToString());
                    rowCounter++;
                }
            }
            
            excelFile.SaveAs(path + "Reports.xlsx");
            Console.WriteLine("Done!");
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