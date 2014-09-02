namespace Airports.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Airports")]
    public class Airport
    {
        private ICollection<Flight> arrivalFlights;
        private ICollection<Flight> departureFlights;
        
        public Airport()
        {
            this.ArrivalFlights = new HashSet<Flight>();
            this.DepartureFlights = new HashSet<Flight>();
        }

        [Key]
        [BsonId]
        public int AirportId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        [Index(IsUnique = true)]
        public string AirportCode { get; set; }

        [InverseProperty("ArrivalAirport")]
        public virtual ICollection<Flight> ArrivalFlights
        {
            get 
            { 
                return this.arrivalFlights; 
            }

            set 
            { 
                this.arrivalFlights = value; 
            }
        }

        [InverseProperty("DepartureAirport")]
        public virtual ICollection<Flight> DepartureFlights
        {
            get 
            { 
                return this.departureFlights; 
            }

            set 
            { 
                this.departureFlights = value; 
            }
        }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
