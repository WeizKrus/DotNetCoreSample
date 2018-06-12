using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using AgEntities.CustomEntities;

namespace AgEntities.DataContext
{
    public class BloggingContext : DbContext
    {
        private bool created = false;

        public BloggingContext()
        { 
            if (!created) {
                created = !created;
                // Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>();
            modelBuilder.Entity<Post>();
            modelBuilder.Entity<AuditEntry>();
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            string sqliteString = @"Data Source=blog.db";
            optionsBuilder.UseSqlite (sqliteString);
        }
    }
}