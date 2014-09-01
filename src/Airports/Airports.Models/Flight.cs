namespace Airports.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }

        [JsonIgnore]
        public virtual Airport DepartureAirport { get; set; }

        [Required]
        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }

        [JsonIgnore]
        public virtual Airport ArrivalAirport { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string FlightCode { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }

        [Required]
        public double DurationHours { get; set; }

        [ForeignKey("Airline")]
        public int? AirlineId { get; set; }

        [JsonIgnore]
        public virtual Airline Airline { get; set; }
    }
}