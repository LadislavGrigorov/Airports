namespace Airports.Data
{
    using Airports.Data.Repositories;
    using Airports.Models;
    using System;
    using System.Collections.Generic;

    public class AirportsData : IAirportsData
    {
        private IAirportsDbContext dbContext;
        private IDictionary<Type, object> repositories;

        public AirportsData()
            : this(new AirportsDbContext())
        {
            
        }

        public AirportsData(IAirportsDbContext context)
        {
            this.dbContext = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public AirportsRepository Airports
        {
            get 
            {
                return (AirportsRepository)this.GetRepository<Airport>();
            }
        }

        public AirlinesRepository Airlines
        {
            get 
            {
                return (AirlinesRepository)this.GetRepository<Airline>();
            }
        }

        public FlightsRepository Flights
        {
            get 
            {
                return (FlightsRepository)this.GetRepository<Flight>();
            }
        }

        public IRepository<City> Cities
        {
            get 
            {
                return this.GetRepository<City>();
            }
        }

        public IRepository<Country> Countries
        {
            get 
            {
                return this.GetRepository<Country>();
            }
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(Repository<T>);

                if (typeOfModel.IsAssignableFrom(typeof(Airport)))
                {
                    type = typeof(AirportsRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Airline)))
                {
                    type = typeof(AirlinesRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Flight)))
                {
                    type = typeof(FlightsRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(City)))
                {
                    type = typeof(Repository<City>);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Country)))
                {
                    type = typeof(Repository<Country>);
                }

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
