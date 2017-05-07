using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Context
{
    public class FinalProjectDbContext : DbContext
    {
        public FinalProjectDbContext() : base("Server=(local);Database=FinalProject;Trusted_Connection=True;MultipleActiveResultSets=True;")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FinalProjectDbContext>(null);

            modelBuilder.Entity<User>();


            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Messager);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Messaged);


            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Message);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User);


            modelBuilder.Entity<Follower>()
                .HasRequired(f => f.Following);

            modelBuilder.Entity<Follower>()
                .HasRequired(f => f.Followed);


            modelBuilder.Entity<Like>()
                .HasRequired(l => l.Comment);

            modelBuilder.Entity<Like>()
                .HasRequired(l => l.User);

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Follower> Followers { get; set; }

        public virtual DbSet<Like> Likes { get; set; }
    }
}
