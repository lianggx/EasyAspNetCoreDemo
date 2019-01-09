using System;
using System.Collections.Generic;

namespace Ron.MSSQL.DbModels
{
    public partial class Posts
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual Topics Topic { get; set; }
    }
}
