namespace Airports.Data.SqlServer.Repositories
{
    using Airports.Models;
    using System.Linq;

    public class FlightsRepository : Repository<Flight>
    {
         public FlightsRepository(IAirportsDbContextSqlServer dbContext)
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
