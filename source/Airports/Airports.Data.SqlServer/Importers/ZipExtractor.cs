namespace Airports.Data.SqlServer.Importers
{
    using Ionic.Zip;

    public class ZipExtractor
    {
        public void Extract(string sourcePath, string destinationPath)
        {
            using (ZipFile zip = ZipFile.Read(sourcePath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
