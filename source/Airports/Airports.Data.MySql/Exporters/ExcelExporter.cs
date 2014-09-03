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
        public static void GenerateCompositeReport(string path)
        {
            Console.WriteLine("Generating merged report from SQLite and MySql...");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            SLDocument excelFile = new SLDocument();
            using (AirportsDbContextSQLite sqliteDbContext = new AirportsDbContextSQLite())
            {
                using (AirportsDbContextMySql mysqlDbContext = new AirportsDbContextMySql())
                {
                    var mysqlData = mysqlDbContext.Airlinereports.ToList();
                    var sqliteData = sqliteDbContext.Airlines.ToList();

                    int rowCounter = 1;
                    foreach (var row in mysqlData)
                    {
                        excelFile.SetCellValue("A" + rowCounter, row.AirlineId.ToString());
                        excelFile.SetCellValue("B" + rowCounter, row.AirlineName);
                        excelFile.SetCellValue("C" + rowCounter, row.TotalFlightsCount.ToString());
                        excelFile.SetCellValue("D" + rowCounter, row.AverageFlightsCount.ToString());
                        excelFile.SetCellValue("E" + rowCounter, row.TotalFlightsDuration.ToString());
                        excelFile.SetCellValue("F" + rowCounter, row.StartDate.ToString());
                        excelFile.SetCellValue("G" + rowCounter, row.EndDate.ToString());
                        excelFile.SetCellValue("H" + rowCounter, sqliteData.Where(a => a.Name == row.AirlineName).First().Website);
                        excelFile.SetCellValue("I" + rowCounter, sqliteData.Where(a => a.Name == row.AirlineName).First().FoundationYear);
                        rowCounter++;
                    }
                }
            }
            
            excelFile.SaveAs(path + "Reports.xlsx");
            Console.WriteLine("Done!");
        }
    }
}