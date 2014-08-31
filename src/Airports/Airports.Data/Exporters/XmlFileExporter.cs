namespace Airports.Data.Exporters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class XmlFileExporter
    {
        private const string ReportsFolderPath = @"..\..\..\..\Exports\Xml-Reports\";

        public static void GenerateAirlinesReport(AirportsData database)
        {
            ExportHelper.CreateDirectoryIfNotExists(ReportsFolderPath);

            var airlines = database.Airlines.GetAll().ToList();

            var flights = database.Flights.GetAll().Select(flight => new
            {
                AirlineID = flight.AirlineId,
                DepartureAirport = flight.DepartureAirport.Name,
                ArivalAirport = flight.ArrivalAirport.Name,
                Code = flight.FlightCode
            }).ToList();

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
