namespace Airports.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cities")]
    public class City
    {
        [Key]
        public int CityId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
