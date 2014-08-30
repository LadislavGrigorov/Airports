namespace Airports.Data.Importers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ExcelDataImporter
    {
        private const string WorksheetFileExtensionPattern = @".xls[x]?\b";
        private const string FlightsWorksheetPattern = @"\b\w{3}-(Departures|Arrivals)-\d{2}-\w{3}-\d{4}.xls[x]?\b";

        public void ImportFlightsDataFromDirectory(string directoryPath)
        {
            var filePaths = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                                    .Where(p => Regex.IsMatch(p, FlightsWorksheetPattern));

            foreach (var path in filePaths)
            {
                Console.WriteLine(path);
                this.ImportFlightsDataFromFile(path);
            }
        }

        public void ImportFlightsDataFromFile(string filePath)
        {

        }
    }
}