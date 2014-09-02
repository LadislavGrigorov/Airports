using Airports.Models;
using System.Linq;

namespace Airports.Data.SqlServer.Repositories
{
    public class AirlinesRepository : Repository<Airline>
    {
        public AirlinesRepository(IAirportsDbContextSqlServer dbContext)
            : base(dbContext)
        {
        }
        public override void Add(Airline entity)
        {
            if (this.SearchFor(a => a.AirlineId == entity.AirlineId).Count() == 0)
            {
                base.Add(entity);   
            }
        }
        public override Airline GetById(int id)
        {
            return this.GetAll().FirstOrDefault(a => a.AirlineId.Equals(id));
        }
    }
}
