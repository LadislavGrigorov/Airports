namespace Airports.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cities")]
    public class City
    {
        [Key]
        [BsonId]
        public int CityId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [BsonIgnore]
        public virtual Country Country { get; set; }
    }
}
