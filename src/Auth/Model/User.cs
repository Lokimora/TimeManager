using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongo;
using MongoDB.Bson;

namespace Auth.Model
{
    public class User : PrimaryKeyModel
    {

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime Registration { get; set; }

    }
}
