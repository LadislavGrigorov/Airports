namespace Airports.Data.Exporters
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlFileExporter
    {
        private const string ReportsFolderPath = @"..\..\..\..\Exports\Xml-Reports\";

        public static void GenerateAirlinesReport(IAirportsData database)
        {
            ExportHelper.CreateDirectoryIfNotExists(ReportsFolderPath);

            var airlines = database.Airlines.GetAll().ToList();

            XDocument xdoc = new XDocument(new XElement("airlines",
                from airline in airlines
                select new XElement("airline",
                    new XAttribute("name", airline.Name),
                    new XElement("flights",
                from flight in airline.Flights
                select new XElement("flight",
                    new XAttribute("code", flight.FlightCode),
                    new XElement("departure-airport", flight.DepartureAirport.Name),
                    new XElement("arival-airport", flight.ArrivalAirport.Name),
                    new XElement("duration", flight.DurationHours + " hours"))))));

            string fileName = string.Format(@"{0}airlines{1}.xml", ReportsFolderPath, DateTime.Now.ToBinary());
            xdoc.Save(fileName);
        }
    }
}
