using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserCenterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            int code = 0;
            string userName = string.Empty;
            switch (id)
            {
                case 100:
                    userName = "Ron.liang";
                    break;
                default:
                    userName = "Guest";
                    code = 403;
                    break;
            }

            return new JsonResult(new { code, userName });
        }
    }
}
