namespace Airports.Data.SqlServer.Importers
{
    using System;    
    using System.IO;
    using Airports.Models.MySql;
    using Newtonsoft.Json;

    public static class MySqlReportsImporter
    {
        public static void ImportJsonReport(string path)
        {
            Console.WriteLine("Loading JSON reports into the MySql database");
            string[] files = GetReportsFileNames(path);

            using (EntitiesModel dbContext = new EntitiesModel())
            {
                foreach (var file in files)
                {
                    string fileContent = File.ReadAllText(file);
                    Jsonreport report = JsonConvert.DeserializeObject<Jsonreport>(fileContent);
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
