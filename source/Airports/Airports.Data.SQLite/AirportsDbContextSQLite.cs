namespace Airports.Data.SQLite
{
    using System.Data.Entity;
    using Airports.Models.SQLite;

    public class AirportsDbContextSQLite : DbContext
    {
        public AirportsDbContextSQLite()
            : base("AirportsSQLiteConnectionString")
        {
        }

        public IDbSet<Airline> Airlines { get; set; }
    }
}