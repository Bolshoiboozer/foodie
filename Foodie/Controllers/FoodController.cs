using Foodie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Foodie.Controllers
{
    public class FoodController : ApiController
    {
        // GET: api/Foods
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [ActionName("rndFood")]
        public List<Food> getRandomFoods()
        {
            List<Food> myFoods = Food.CreateRandomFood();
            return myFoods;
        }

        [HttpGet]
        [ActionName("getFoods")]
        public List<Food> getFoods()
        {
            return null;
        }

        // POST: api/Foods
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Foods/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Foods/5
        public void Delete(int id)
        {
        }
    }
}
