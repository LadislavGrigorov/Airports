namespace Airports.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }

        [InverseProperty("DepartureFlights")]
        public Airport DepartureAirport { get; set; }

        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }

        [InverseProperty("ArrivalFlights")]
        public Airport ArrivalAirport { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string FlightCode { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }

        [Required]
        public double DurationHours { get; set; }

        [ForeignKey("Airline")]
        public int AirlineId { get; set; }

        [InverseProperty("Flights")]
        public virtual Airline Airline { get; set; }
    }
}