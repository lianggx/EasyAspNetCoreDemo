using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ron.JsonTest.Models;

namespace Ron.JsonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            UserInfo info = new UserInfo()
            {
                Age = 22,
                Gender = true,
                Name = "Ron.lang",
                RegTime = DateTime.Now
            };
            return JsonReturn.成功.SetData("detail", info);
        }
    }
}
