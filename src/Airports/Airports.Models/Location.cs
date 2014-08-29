namespace Airports.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ComplexType]
    public class Location
    {
        [Column()]
        public string City { get; set; }

        public string Country { get; set; }

        public string AddressText { get; set; }
    }
}