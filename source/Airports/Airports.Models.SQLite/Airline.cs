namespace Airports.Models.SQLite
{
    using System.ComponentModel.DataAnnotations;

    public class Airline
    {
        [Key]
        public long AirlineId { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public string FoundationYear { get; set; }
    }
}
