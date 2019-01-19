using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.OtherDB.Models
{
    public class NPgSqlForumContext : DbContext
    {
        public NPgSqlForumContext(DbContextOptions<NPgSqlForumContext> options) : base(options) { }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
