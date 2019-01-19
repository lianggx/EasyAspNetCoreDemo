using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ron.OtherDB.Models;
using Ron.OtherDB.ViewModel;

namespace Ron.OtherDB.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class HomeController : ControllerBase
    {
        private MySqlForumContext mysqlContext;
        private NPgSqlForumContext pgsqlContext;
        public HomeController(MySqlForumContext mysqlContext, NPgSqlForumContext pgsqlContext)
        {
            this.mysqlContext = mysqlContext;
            this.pgsqlContext = pgsqlContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            // MySql
            var mysqlTopics = this.mysqlContext.Topics.ToList();

            // PgSql
            var pgsqlTopics = this.pgsqlContext.Topics.ToList();

            return new JsonResult(new { mysql = mysqlTopics, pgsql = pgsqlTopics });
        }

        [HttpPost]
        public async Task Post([FromBody] TopicViewModel model)
        {
            // MySql
            this.mysqlContext.Topics.Add(new Topic()
            {
                Content = model.Content,
                CreateTime = DateTime.Now,
                Title = model.Title
            });
            await this.mysqlContext.SaveChangesAsync();

            // PgSql
            this.pgsqlContext.Topics.Add(new Topic()
            {
                Content = model.Content,
                CreateTime = DateTime.Now,
                Title = model.Title
            });
            await this.pgsqlContext.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put([FromBody] TopicViewModel model)
        {
            // MySql
            var topic = this.mysqlContext.Topics.Where(f => f.Id == model.Id).FirstOrDefault();
            topic.Title = model.Title;
            topic.Content = model.Content;
            await this.mysqlContext.SaveChangesAsync();

            // PgSql
            var pgTopic = this.pgsqlContext.Topics.Where(f => f.Id == model.Id).FirstOrDefault();
            pgTopic.Title = model.Title;
            pgTopic.Content = model.Content;
            await this.pgsqlContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            // MySql
            var topic = this.mysqlContext.Topics.Where(f => f.Id == id).FirstOrDefault();
            this.mysqlContext.Topics.Remove(topic);
            await this.mysqlContext.SaveChangesAsync();

            // PgSql
            var pgTopic = this.pgsqlContext.Topics.Where(f => f.Id == id).FirstOrDefault();
            this.pgsqlContext.Topics.Remove(pgTopic);
            await this.pgsqlContext.SaveChangesAsync();
        }
    }
}
