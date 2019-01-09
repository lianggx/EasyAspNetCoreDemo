using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ron.PhoneTest.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        PhoneValidator validator = null;
        public HomeController(PhoneValidator pv)
        {
            validator = pv;
        }

        [HttpGet("login")]
        public IActionResult Login(string phone)
        {
            bool accept = validator.IsPhone(ref phone);
            return new JsonResult(new { phone, accept });
        }
    }
}