using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ron.MSSQL.Models;
using Ron.MSSQL.ViewModel;

namespace Ron.MSSQL.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class HomeController : ControllerBase
    {
        private ForumContext context;
        public HomeController(ForumContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Topic>> Get()
        {
            var topics = context.Topics.ToList();

            return topics;
        }

        [HttpPost]
        public void Post([FromBody] TopicViewModel model)
        {
            context.Topics.Add(new Topic()
            {
                Content = model.Content,
                CreateTime = DateTime.Now,
                Title = model.Title
            });
            context.SaveChanges();
        }

        [HttpPut]
        public void Put([FromBody] TopicViewModel model)
        {
            var topic = context.Topics.Where(f => f.Id == model.Id).FirstOrDefault();
            topic.Title = model.Title;
            topic.Content = model.Content;
            context.Topics.Update(topic);
            context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var topic = context.Topics.Where(f => f.Id == id).FirstOrDefault();
            context.Topics.Remove(topic);
            context.SaveChanges();
        }
    }
}
