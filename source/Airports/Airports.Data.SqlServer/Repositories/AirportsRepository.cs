namespace Airports.Data.SqlServer.Repositories
{
    using Airports.Models;
    using System.Linq;

    public class AirportsRepository : Repository<Airport>
    {
        public AirportsRepository(IAirportsDbContextSqlServer dbContext)
            : base(dbContext)
        {
        }

        public override void Add(Airport entity)
        {
            if (this.SearchFor(a => a.AirportId == entity.AirportId).Count() == 0)
            {
                base.Add(entity);
            }
        }

        public override Airport GetById(int id)
        {
            return this.GetAll().FirstOrDefault(a => a.AirportId.Equals(id));
        }
    }
}
