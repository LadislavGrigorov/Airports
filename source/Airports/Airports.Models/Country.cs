namespace Airports.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Countries")]
    [BsonIgnoreExtraElements]
    public class Country
    {
        private ICollection<City> cities;

        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [Key]
        [BsonId]
        public int CountryId { get; set; }

        [Required]
        [BsonRequired]
        public string Name { get; set; }

        [BsonIgnore]
        [InverseProperty("Country")]
        public virtual ICollection<City> Cities 
        {
            get 
            { 
                return this.cities; 
            }

            set 
            { 
                this.cities = value; 
            }
        }
    }
}