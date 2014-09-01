namespace Airports.Data.Exporters
{
    using System.IO;

    public static class ExportHelper
    {
        public static void CreateDirectoryIfNotExists(string path)
        {
            // TODO: Handle possible exceptions
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
