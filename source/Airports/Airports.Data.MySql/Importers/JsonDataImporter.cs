namespace Airports.Data.MySql.Importers
{
    using System;    
    using System.IO;
    using Airports.Models.MySql;
    using Newtonsoft.Json;

    public class JsonDataImporter
    {
        public void ImportAirlineReports(string path)
        {
            Console.WriteLine("Loading JSON reports into the MySql database");
            string[] files = GetReportsFileNames(path);

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

            Console.WriteLine("Done!");
        }

        private static string[] GetReportsFileNames(string path)
        {
            string[] filePaths = Directory.GetFiles(path);
            return filePaths;
        }
    }
}
