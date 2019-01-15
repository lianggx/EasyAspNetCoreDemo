using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.TaskThird.Models
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {

        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override void Dispose()
        {
            base.Dispose();

            Console.WriteLine("Dispose");
        }
    }
}
