using Airports.Models;
using System.Linq;

namespace Airports.Data.Repositories
{
    public class FlightsRepository : Repository<Flight>
    {
         public FlightsRepository(IAirportsDbContext dbContext)
            : base(dbContext)
        {
        }

         public override void Add(Flight entity)
         {
             if (this.SearchFor(f => f.FlightCode == entity.FlightCode).Count() == 0)
             {
                 base.Add(entity);
             }
         }

        public override Flight GetById(int id)
        {
            return this.GetAll().FirstOrDefault(f => f.FlightId.Equals(id));
        }
    }
}
