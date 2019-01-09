using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Ron.MSSQL.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}