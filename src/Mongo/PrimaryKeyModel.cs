using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo
{
    public abstract class PrimaryKeyModel
    {
        private ObjectId _id;

        [BsonId]
        public ObjectId Id
        {
            get
            {
                if (_id == ObjectId.Empty)
                {
                    _id = ObjectId.GenerateNewId();
                }

                return _id;
            }
            set
            {
                _id = ObjectId.GenerateNewId();
            }
        }
    }
}
