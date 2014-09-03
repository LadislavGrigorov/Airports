namespace Airports.Data.MySql.Exporters
{
    using System;
    using System.IO;
    using System.Linq;

    using Airports.Data.SQLite;
    using Airports.Models.MySql;
    using SpreadsheetLight;

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
                    var compositeReports = from report in mysqlDbContext.Airlinereports.AsEnumerable()
                                        join airline in sqliteDbContext.Airlines.AsEnumerable()
                                        on report.AirlineName equals airline.Name
                                        select 
                                        new {
                                                report.AirlineName,
                                                report.TotalFlightsCount,
                                                report.AverageFlightsCount,
                                                report.TotalFlightsDuration,
                                                report.StartDate,
                                                report.EndDate,
                                                airline.Website,
                                                airline.FoundationYear
                                            };

                    excelFile.SetCellValue("A1", "Airline Name");
                    excelFile.SetCellValue("B1", "Total Flights Count"); 
                    excelFile.SetCellValue("C1", "Average Flights Duration");
                    excelFile.SetCellValue("D1", "Total Flights Duration"); 
                    excelFile.SetCellValue("E1", "From Date");
                    excelFile.SetCellValue("F2", "To Date"); 
                    excelFile.SetCellValue("G1", "Company Website");
                    excelFile.SetCellValue("H1", "Foundation Year");

                    int rowCounter = 2;
                    foreach (var report in compositeReports)
                    {
                        excelFile.SetCellValue("A" + rowCounter, report.AirlineName);
                        excelFile.SetCellValue("B" + rowCounter, report.TotalFlightsCount.ToString());
                        excelFile.SetCellValue("C" + rowCounter, report.AverageFlightsCount.ToString());
                        excelFile.SetCellValue("D" + rowCounter, report.TotalFlightsDuration.ToString());
                        excelFile.SetCellValue("E" + rowCounter, report.StartDate.ToString());
                        excelFile.SetCellValue("F" + rowCounter, report.EndDate.ToString());
                        excelFile.SetCellValue("G" + rowCounter, report.Website.ToString());
                        excelFile.SetCellValue("H" + rowCounter, report.FoundationYear.ToString());
                        rowCounter++;
                    }
                }
            }
            
            excelFile.SaveAs(path + "Reports.xlsx");
            Console.WriteLine("Done!");
        }
    }
}