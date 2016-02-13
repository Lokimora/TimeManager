using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo
{

    public class DbCollection<T> where T : PrimaryKeyModel, new()
    {
        protected DB _db;

        private IMongoCollection<T> _collection;


        public DbCollection(DB db)
        {
            _db = db;



            var collection = _db.Database.GetCollection<T>(nameof(T));

            if (collection == null)
            {
                _db.Database.CreateCollection(nameof(T), new CreateCollectionOptions());
            }

            _collection = _db.Database.GetCollection<T>(nameof(T));

        }


        public async Task Insert(T t)
        {
            await _collection.InsertOneAsync(t);
        }

        public async Task Update(T t)
        {
            FilterDefinition<T> filter = new ObjectFilterDefinition<T>(t);

            UpdateDefinition<T> update = new ObjectUpdateDefinition<T>(t);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<T> FindOne(ObjectId id)
        {
            var cursor = await _collection.FindAsync(new ObjectFilterDefinition<T>(new T() { Id = id }));

            return await cursor.FirstAsync();
        }

        public async Task Delete(ObjectId id)
        {
            await _collection.FindOneAndDeleteAsync(new ObjectFilterDefinition<T>(new T() { Id = id }));
        }




    }
}
