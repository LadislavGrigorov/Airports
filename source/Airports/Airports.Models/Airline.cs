namespace Airports.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization;

    [Table("Airlines")]
    public class Airline
    {
        private ICollection<Flight> flights;

        public Airline()
        {
            this.Flights = new HashSet<Flight>();
        }

        [Key]
        [BsonId]
        public int AirlineId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [BsonIgnore]
        [InverseProperty("Airline")]
        public virtual ICollection<Flight> Flights
        {
            get 
            { 
                return this.flights; 
            }

            set 
            {
                this.flights = value; 
            }
        }
    }
}