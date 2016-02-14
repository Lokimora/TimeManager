using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Mongo;
using TimeManager.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbCollection<TestModel> _testCollection;

        public HomeController(DbCollection<TestModel> testCollection)
        {
            _testCollection = testCollection;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var testMod = new TestModel()
            {
                Name = "Hello World",
                Time = DateTime.Now
            };

            await _testCollection.InsertAsync(testMod);

            return new EmptyResult();
        }
    }
}
