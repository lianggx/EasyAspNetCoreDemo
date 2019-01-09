using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ron.LogFilter.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            throw new Exception("出错了.....");
            return new string[] { "value1", "value2" };
        }
    }
}
