using BoardProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardProject
{
    public class PostDbContext : DbContext
    {
        public PostDbContext()
        {

        }

        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
            .Property(p => p.CreatedTime)
            .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<CommentModel>()
            .Property(p => p.LikeNum)
            .HasDefaultValue(0);

            modelBuilder.Entity<CommentModel>()
            .Property(p => p.CommentDateTime)
            .HasDefaultValueSql("GetDate()");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
    }
}
