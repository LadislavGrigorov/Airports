namespace Airports.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("Countries")]
    public class Country
    {
        private ICollection<City> cities;

        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [Key]
        public int CountryId { get; set; }

        [Required]
        public string Name { get; set; }

        [InverseProperty("Country")]
        public virtual ICollection<City> Cities 
        {
            get { return this.cities; }
            set { this.cities = value; }
        }
    }
}