using MongoDB.Driver;

namespace APIAnallyzer_v2.Data
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        
        public MongoDbService(IConfiguration configuration) 
        { 
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
        
        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoDatabase? Database => _database;
    }
}