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
        Task<User> GetByIdAsync(ObjectId id);

        Task<User> GetByEmailAsync(string email);

        Task<bool> IsExistAsync(string email);

        Task CreateAsync(User user);

        Task UpdateAsync(User user, params Expression<Func<User, object>>[] fields);

        
    }
}
