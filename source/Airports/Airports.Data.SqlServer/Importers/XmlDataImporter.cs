namespace Airports.Data.SqlServer.Importers
{
    using Airports.Models;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class XmlDataImporter
    {
        private const string XmlFileExtensionPattern = @".xml\b";
        private const string FlightsXmlFilePattern = @"\w{3}-(Departures|Arrivals)-\d{2}-\w{3}-\d{4}.xml\b";
        private const string InvalidFileNameMessage =
            "Provided file name is either invalid or does not match the naming convention for an xml [{0}] data file.";
        
        public ICollection<Flight> ImportFlightsDataFromFile(string filePath)
        {
            if (!Regex.IsMatch(filePath, FlightsXmlFilePattern))
            {
                throw new ArgumentException(string.Format(InvalidFileNameMessage, "Flights"));
            }

            ICollection<Flight> importedFlights = new HashSet<Flight>();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    try
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "flight")
                        {
                            int flightId = int.Parse(reader.GetAttribute("id"));
                            string flightCode = reader.GetAttribute("code").ToString();
                            DateTime flightDate = DateTime.Parse(reader.GetAttribute("dateTime"));

                            reader.ReadToDescendant("airline");
                            int airlineId = int.Parse(reader.GetAttribute("airlineId"));

                            reader.ReadToNextSibling("departureAirport");
                            int departureAirportId = int.Parse(reader.GetAttribute("departureAirportId"));

                            reader.ReadToNextSibling("arrivalAirport");
                            int arrivalAirportId = int.Parse(reader.GetAttribute("arrivalAirportId"));

                            reader.ReadToNextSibling("duration");
                            double durationHours = reader.ReadElementContentAsDouble();

                            var flight = new Flight()
                            {
                                FlightId = flightId,
                                FlightCode = flightCode,
                                AirlineId = airlineId,
                                FlightDate = flightDate,
                                DurationHours = durationHours,
                                DepartureAirportId = departureAirportId,
                                ArrivalAirportId = arrivalAirportId
                            };

                            importedFlights.Add(flight);
                        }
                    }
                    catch (FormatException)
                    { }
                }    
            }

            return importedFlights;
        }
    }
}
