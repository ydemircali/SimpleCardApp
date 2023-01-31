using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardApp.Application.Services
{
    public class MongoDbService
    {
        readonly IMongoDatabase _database;
        readonly IConfiguration _configuration;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            MongoClient client = new(_configuration["MongoDB:Server"]);
            _database = client.GetDatabase(_configuration["MongoDB:DBName"]);
        }
        public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}
