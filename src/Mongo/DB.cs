using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo
{

    public class DB
    {

        private IMongoClient _client;

        private IMongoDatabase _database;

        protected DB(string connectionString)
        {
            var url = MongoUrl.Create(connectionString);

            _client = new MongoClient(url);

            _database = _client.GetDatabase(url.DatabaseName);

        }


        public IMongoDatabase Database => _database;

        public IMongoClient Client => _client;


    }
}
