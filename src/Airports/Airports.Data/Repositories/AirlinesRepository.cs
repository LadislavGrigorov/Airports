using Airports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airports.Data.Repositories
{
    class AirlinesRepository : Repository<Airline>
    {
        public AirlinesRepository(IAirportsDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Airline GetById(int id)
        {
            return this.GetAll().Single(a => a.AirlineId.Equals(id));
        }
    }
}
