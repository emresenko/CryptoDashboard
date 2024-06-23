using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CryptoDashboard.Api.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly IMongoCollection<BsonDocument> _logsCollection;

        public LoggingService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            var database = client.GetDatabase("CryptoDashboard");
            _logsCollection = database.GetCollection<BsonDocument>("RequestLogs");
        }

        public async Task LogRequestAsync(DateTime beginDate, DateTime endDate)
        {
            var log = new BsonDocument
            {
                { "BeginDate", beginDate },
                { "EndDate", endDate },
                { "RequestDate", DateTime.UtcNow }
            };

            await _logsCollection.InsertOneAsync(log);
        }
    }
}
