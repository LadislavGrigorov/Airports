namespace Airports.Data.MongoDb
{
    using MongoDB.Driver;

    public interface IAirportsContextMongoDb
    {
        MongoCollection GetCollection(string collectionName);

        void SaveToCollection<T>(string collectionName, T item);
    }
}
