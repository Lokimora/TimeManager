using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mongo.Helpers;
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

            var collectioName = typeof (T).Name;

            var collection = _db.Database.GetCollection<T>(collectioName);

            if (collection == null)
            {
                _db.Database.CreateCollection(collectioName, new CreateCollectionOptions());
            }

            _collection = _db.Database.GetCollection<T>(collectioName);

        }

        #region AsyncMethods

        public async Task InsertAsync(T t)
        {
            await _collection.InsertOneAsync(t);
        }

        public async Task UpdateAsync(T t, params Expression<Func<T, object>>[] fields)
        {
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(p => p.Id, t.Id);

            var update = Builders<T>.Update.Combine(fields.Select(p =>
            {
                var value = p.Compile().Invoke(t);

                return Builders<T>.Update.Set(p.GetPropertyName(), value);

            }));

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<T> FindOneAsync(ObjectId id)
        {
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(p => p.Id, id);

            var cursor = await _collection.FindAsync(filter);

            return await cursor.FirstAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(p => p.Id, id);

            await _collection.FindOneAndDeleteAsync(filter);
        }

        #endregion

        #region SyncMethods

        public void Insert(T t)
        {
            _collection.InsertOne(t);
        }

        public void Update(T t, params Expression<Func<T, object>>[] fields)
        {
            UpdateAsync(t, fields).RunSynchronously();
        }

        public T FindOne(ObjectId id)
        {
            return FindOneAsync(id).Result;
        }

        public void Delete(ObjectId id)
        {
            DeleteAsync(id).RunSynchronously();
        }

        #endregion









    }
}
