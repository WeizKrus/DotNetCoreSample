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

        public BloggingContext ()
        {
            if (!created)
            {
                created = !created;
                // Database.EnsureDeleted();
                Database.EnsureCreated ();
            }
        }

        // Defines the tables
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            // Use Fluent API to indicate a property is required
            modelBuilder.Entity<Blog> ()
                .Property (b => b.Url)
                .IsRequired ();

            modelBuilder.Entity<Post> ();

            modelBuilder.Entity<Audit> ();
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            string sqliteString = @"Data Source=blog.db";
            optionsBuilder.UseSqlite (sqliteString);
        }

        public override int SaveChanges() 
        {
            var auditEntries = OnBeforeSaveChanges ();
            var result = base.SaveChanges();
            OnAfterSaveChanges (auditEntries);

            return result;
        }

        List<AuditEntry> OnBeforeSaveChanges ()
        {
            ChangeTracker.DetectChanges ();

            var auditEntries = new List<AuditEntry> ();

            foreach (var entry in ChangeTracker.Entries ())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry (entry);
                auditEntry.TableName = entry.Metadata.Relational ().TableName;
                auditEntries.Add (auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add (property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey ())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where (_ => !_.HasTemporaryProperties))
            {
                Audits.Add (auditEntry.ToAudit ());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where (_ => _.HasTemporaryProperties).ToList ();
        }

        private int OnAfterSaveChanges (List<AuditEntry> auditEntries)
        {
            // if (auditEntries == null || auditEntries.Count == 0)
            // {
            //     return Task.CompletedTask;
            // }

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey ())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add (auditEntry.ToAudit ());
            }

            // return SaveChangesAsync ();
            return SaveChanges();
        }
    }
}