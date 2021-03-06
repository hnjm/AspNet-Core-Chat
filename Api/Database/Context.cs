using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Database
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public Context(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.Email).IsUnique();

                b.HasMany(u => u.Messages).WithOne(m => m.Sender).HasForeignKey(u => u.SenderId);
            });

            modelBuilder.Entity<Message>(b =>
            {
                b.HasOne(m => m.Sender).WithMany(u => u.Messages).HasForeignKey(m => m.SenderId);
            });
        }
    }
}
