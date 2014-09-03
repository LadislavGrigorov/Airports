namespace Airports.SqlServer.Data.Exporters
{
    using Airports.Data.SqlServer;
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlFileExporter
    {
        public static void GenerateAirlinesReport(string filePath, IAirportsDataSqlServer database)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var airlines = database.Airlines.GetAll()
                .Include("Flights")
                .Select(airline => new
                {
                    Name = airline.Name,
                    Flights = airline.Flights.Select(flight =>
                    new
                    {
                        FlightCode = flight.FlightCode,
                        DepartureAirport = flight.DepartureAirport,
                        ArrivalAirport = flight.ArrivalAirport,
                        DurationHours = flight.DurationHours
                    })

                })
                .ToList();


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

            DateTime now = DateTime.Now;
            string uniqueComponent = string.Format("{0}.{1}.{2}-{3}.{4}.{5}-{6}", 
                now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
            string fileName = string.Format(@"{0}airlines-{1}.xml", filePath, uniqueComponent);
            xdoc.Save(fileName);
        }
    }
}
