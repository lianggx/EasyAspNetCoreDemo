using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ron.FilterDemo.Filters;
using Ron.FilterDemo.Models;

namespace Ron.FilterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomerResourceFilter]
    [CustomerExceptionFilter]
    public class HomeController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [CustomerActionFilter]
        [CustomerResultFilter]

        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [CustomerActionFilter]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [UserNameActionFilter(Order = 10)]
        [UserAgeActionFilter(Order = 5)]
        public void Post([FromBody] UserModel value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
