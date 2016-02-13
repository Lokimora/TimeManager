using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Auth.Model;
using Mongo;
using MongoDB.Bson;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly DbCollection<User> _userCollection;

        public UserService(DbCollection<User> userCollection)
        {
            _userCollection = userCollection;
        }

        public async User GetById(ObjectId id)
        {
            return _userCollection.FindOne(id);
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(string email)
        {
            throw new NotImplementedException();
        }

        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User user, params Expression<Func<User, object>>[] fields)
        {
            throw new NotImplementedException();
        }
    }
}
