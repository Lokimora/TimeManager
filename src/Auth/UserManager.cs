using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Model;
using Mongo;

namespace Auth
{
    public class UserManager
    {
        private readonly DbCollection<User> _userCollection;

        public UserManager(DbCollection<User> userCollection)
        {
            _userCollection = userCollection;
        }

        public void CreateUser(User user)
        {
            
        }
        
    }
}
