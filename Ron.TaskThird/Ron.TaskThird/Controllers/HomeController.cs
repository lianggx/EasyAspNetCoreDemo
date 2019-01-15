using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Ron.TaskThird.Models;
using Ron.TaskThird.ViewModels;

namespace Ron.TaskThird.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ForumContext context;
        public HomeController(ForumContext context)
        {
            this.context = context;
        }

        // PUT api/values/5
        [HttpPut]
        public async Task Put([FromBody] TopicViewModel model)
        {
            var topic = this.context.Topics.Where(f => f.Id == model.Id).FirstOrDefault();
            topic.Content = model.Content;
            this.context.Update(topic);
            Console.WriteLine("Updated");
            var affrows = await this.context.SaveChangesAsync();
            Console.WriteLine("affrows:{0}", affrows);
        }
    }
}
