namespace Airports.Data.SqlServer.Exporters
{
    using Airports.Models.MySql;
    using SpreadsheetLight;
    using System;
    using System.IO;
    using System.Linq;

    public static class ExcelReportExporter
    {
        public static void GenerateExcelFile(string path)
        {
            Console.WriteLine("Generating merged report from SQLite and MySql...");

            CreateDirectoryIfNotExists(path);

            SLDocument excelFile = new SLDocument();

            using (EntitiesModel dbContext = new EntitiesModel())
            {
                var allData = dbContext.Jsonreports.ToList();

                int rowCounter = 1;
                foreach (var row in allData)
                {
                    excelFile.SetCellValue("A" + rowCounter, row.ReportId);
                    excelFile.SetCellValue("B" + rowCounter, row.FlightCode);
                    excelFile.SetCellValue("C" + rowCounter, row.FlightDate.ToString());
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