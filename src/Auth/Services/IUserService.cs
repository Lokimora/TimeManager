using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Auth.Model;
using MongoDB.Bson;

namespace Auth.Services
{
    public interface IUserService
    {
        User GetById(ObjectId id);

        User GetByEmail(string email);

        bool IsExist(string email);

        void Create(User user);

        void Update(User user, params Expression<Func<User, object>>[] fields);
    }
}
