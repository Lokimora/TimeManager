using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Auth.Helper;
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

        public async Task<User> GetByIdAsync(ObjectId id)
        {
            return await _userCollection.FindOneAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userCollection.FindOneByConditionAsync(p => p.Email == email);
        }

        public async Task<bool> IsExistAsync(string email)
        {
            var user = await _userCollection.FindOneByConditionAsync(p => p.Email == email);

            if (user != null)
                return true;

            return false;
        }

        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertAsync(user);
        }

        public async Task UpdateAsync(User user, params Expression<Func<User, object>>[] fields)
        {
            await _userCollection.UpdateAsync(user, fields);
        }

        public User GetById(ObjectId id)
        {
            return GetByIdAsync(id).Result;
        }

        public User GetByEmail(string email)
        {
            return GetByEmailAsync(email).Result;
        }
    }
}
