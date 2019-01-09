using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ron.TestDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IConfiguration configuration;
        public ValuesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            var result= id + this.configuration.GetValue<int>("max");

            return result;
        }
    }
}
