namespace Airports.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson.Serialization.Attributes;

    [Table("Flights")]
    public class Flight
    {
        [Key]
        [BsonId]
        public int FlightId { get; set; }

        [Required]
        [BsonRequired]
        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        [BsonIgnoreIfNull]
        public virtual Airport DepartureAirport { get; set; }

        [Required]
        [BsonRequired]
        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        [BsonIgnoreIfNull]
        public virtual Airport ArrivalAirport { get; set; }

        [Required]
        [BsonRequired]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string FlightCode { get; set; }

        [Required]
        [BsonRequired]
        public DateTime FlightDate { get; set; }

        [Required]
        [BsonRequired]
        public double DurationHours { get; set; }

        [ForeignKey("Airline")]
        public int? AirlineId { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        [BsonIgnoreIfNull]
        public virtual Airline Airline { get; set; }
    }
}