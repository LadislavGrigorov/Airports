namespace Airports.Data.MySql.Importers
{
    using System;    
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Airports.Models.MySql;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class JsonDataImporter
    {
        private const string JsonFileExtensionPatter = @".json\b";

        public void ImportAirlineReportsFromDirectory(string path)
        {
            var files = GetReportsFileNamesFromDirectory(path);

            using (AirportsDbContextMySql dbContext = new AirportsDbContextMySql())
            {
                foreach (var file in files)
                {
                    string fileContent = File.ReadAllText(file);
                    Airlinereport report = JsonConvert.DeserializeObject<Airlinereport>(fileContent);
                    dbContext.Add(report);
                }

                dbContext.SaveChanges();
            }
        }

        private static IEnumerable<string> GetReportsFileNamesFromDirectory(string directoryPath)
        {
            IEnumerable<string> filePaths = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                            .Where(p => Regex.IsMatch(p, JsonFileExtensionPatter));

            return filePaths;
        }
    }
}
