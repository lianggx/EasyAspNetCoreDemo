using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ron.FilterDemo.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ron.FilterDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [MiddlewareFilter(typeof(RegisterManagerPipeline))]
    public class UserController : Controller
    {
        // GET: api/<controller>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "default";
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
