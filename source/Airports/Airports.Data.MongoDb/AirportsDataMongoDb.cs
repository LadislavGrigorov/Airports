namespace Airports.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Airports.Models;
    using MongoDB.Driver.Linq;
    
    public class AirportsDataMongoDb : IAirportsDataMongoDb
    {
        private const string AirportsCollectionName = "Airports";
        private const string AirlinesCollectionName = "Airlines";
        private const string FlightsCollectionName = "Flights";
        private const string CitiesCollectionName = "Cities";
        private const string CountriesCollectionName = "Countries";

        private readonly IDictionary<Type, string> collectionNames = new Dictionary<Type, string>()
        {
            { typeof(Airport), AirportsCollectionName },
            { typeof(Airline), AirlinesCollectionName },
            { typeof(Flight), FlightsCollectionName },
            { typeof(City), CitiesCollectionName },
            { typeof(Country), CountriesCollectionName }
        };

        private IAirportsContextMongoDb dbContext;

        public AirportsDataMongoDb() : this(new AirportsContextMongoDb())
        {
        }

        public AirportsDataMongoDb(IAirportsContextMongoDb context)
        {
            this.dbContext = context;
        }

        public IQueryable<Airport> Airports
        {
            get
            {
                return this.dbContext.GetCollection(AirportsCollectionName).AsQueryable<Airport>();
            }
        }

        public IQueryable<Airline> Airlines
        {
            get
            {
                return this.dbContext.GetCollection(AirlinesCollectionName).AsQueryable<Airline>();
            }
        }

        public IQueryable<Flight> Flights
        {
            get
            {
                return this.dbContext.GetCollection(FlightsCollectionName).AsQueryable<Flight>();
            }
        }

        public IQueryable<City> Cities
        {
            get
            {
                return this.dbContext.GetCollection(CitiesCollectionName).AsQueryable<City>();
            }
        }

        public IQueryable<Country> Countries
        {
            get
            {
                return this.dbContext.GetCollection(CountriesCollectionName).AsQueryable<Country>();
            }
        }

        public void Save<T>(T item)
        {
            var type = typeof(T);

            if (!this.collectionNames.ContainsKey(type))
            {
                throw new ArgumentException("There is no collection with items of this type in the database.");
            }

            this.dbContext.SaveToCollection(this.collectionNames[type], item);
        }
    }
}