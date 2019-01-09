using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ron.MSSQL.DbModels
{
    public partial class ForumContext : DbContext
    {
        public ForumContext()
        {
        }

        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;uid=sa;pwd=123456;database=Forum");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasIndex(e => e.TopicId);

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.TopicId);
            });
        }
    }
}
