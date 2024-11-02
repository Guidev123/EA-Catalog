using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CatalogService.Infrastructure.Persistence
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            string connectionString = _configuration.GetConnectionString("DbConnection") ?? string.Empty;

            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mongoClient = new(mongoUrl);

            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
        public IMongoDatabase? Database => _database;
    }
}
