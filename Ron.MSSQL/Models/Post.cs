using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.MSSQL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
