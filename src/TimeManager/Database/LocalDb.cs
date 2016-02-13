using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongo;

namespace TimeManager.Database
{
    public class LocalDb : DB
    {
        public LocalDb(string connectionString) : base(connectionString)
        {

        }
    }
}
