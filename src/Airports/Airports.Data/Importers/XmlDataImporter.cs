namespace Airports.Data.Importers
{
    using Airports.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Text.RegularExpressions;

    public class XmlDataImporter
    {
        private const string InvalidFileNameMessage = @"Provided file name is either invalid or does not match 
                                                    the naming convention for an xml [{0}] data file.";

        private const string XmlFileExtensionPattern = @".xml\b";
        private const string FlightsXmlFilePathPattern = @"\w{3}-(Departures|Arrivals)-\d{2}-\w{3}-\d{4}.xml\b";

        public ICollection<Flight> ImportFlightsDataFromFile(string filePath)
        {
            if (!Regex.IsMatch(filePath, FlightsXmlFilePathPattern))
            {
                throw new ArgumentException(string.Format(InvalidFileNameMessage, "Flights"));
            }

            ICollection<Flight> flights = new HashSet<Flight>();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "flight")
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
