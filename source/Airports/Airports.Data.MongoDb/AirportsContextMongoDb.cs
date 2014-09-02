namespace Airports.Data.MongoDb
{
    using MongoDB.Driver;

    internal class AirportsContextMongoDb : IAirportsContextMongoDb
    {
        private MongoDatabase database;

        public AirportsContextMongoDb()
        {
            this.database = this.ConnectToDatabase();
        }

        public MongoCollection GetCollection(string collectionName)
        {
            return this.database.GetCollection(collectionName);
        }

        public void SaveToCollection<T>(string collectionName, T item)
        {
            this.GetCollection(collectionName).Save(item);
        }

        private MongoDatabase ConnectToDatabase()
        {
            var client = new MongoClient(MongoDbSettings.Default.AirportsMongoDbConnectionString);
            MongoServer server = client.GetServer();
            return server.GetDatabase(MongoDbSettings.Default.DatabaseName); 
        }
    }
}
