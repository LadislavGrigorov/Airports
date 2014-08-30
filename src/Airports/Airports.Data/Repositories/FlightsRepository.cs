using Airports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airports.Data.Repositories
{
    class FlightsRepository : Repository<Flight>
    {
         public FlightsRepository(IAirportsDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Flight GetById(int id)
        {
            return this.GetAll().Single(f => f.FlightId.Equals(id));
        }
    }
}
