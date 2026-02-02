using Streamline.Domain.Entities;
using Streamline.Domain.Entities.Logs;
using MongoDB.Driver;

namespace Streamline.Infrastructure.Persistence.MongoDb.DbContexts
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public IMongoCollection<Log> Logs => _database.GetCollection<Log>("audit_logs");

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            InitializeLogCollection();
        }

        private void InitializeLogCollection()
        {
            var existingCollections = _database.ListCollectionNames().ToList();

            if (!existingCollections.Contains("audit_logs"))
            {
    
                var timeSeriesOptions = new TimeSeriesOptions(
                    timeField: "Timestamp",   
                    metaField: "Severity",       
                    granularity: TimeSeriesGranularity.Seconds
                );

                var createOptions = new CreateCollectionOptions
                {
                    TimeSeriesOptions = timeSeriesOptions
                };

                _database.CreateCollection("audit_logs", createOptions);
            }
        }
    }
}
