using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongo;


namespace TimeManager.Model
{
    public class TestModel : PrimaryKeyModel
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }
    }
}
