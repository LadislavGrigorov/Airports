namespace Airports.Data.Loaders
{
    using Ionic.Zip;

    public class ZipExtractor
    {
        public void ExtractZip(string filePath, string destinationPath)
        {
            using (ZipFile zip = ZipFile.Read(filePath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
