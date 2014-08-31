namespace Airports.Data.Repositories
{
    using Airports.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AirportsRepository : Repository<Airport>
    {
        public AirportsRepository(IAirportsDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Airport GetById(int id)
        {
            return this.GetAll().Single(a => a.AirportId.Equals(id));
        }
    }
}
