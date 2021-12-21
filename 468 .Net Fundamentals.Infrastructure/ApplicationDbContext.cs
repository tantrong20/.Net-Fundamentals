using _468_.Net_Fundamentals.Domain;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var users = new User[]
            {
                new User {Id =1, Name = "Tan Trong", Email = "tronglt2001@gmail.com", Role = (int)Role.Employee},
                new User {Id =2, Name = "Hien Nhu", Email = "hiennhu@gmail.com", Role = (int)Role.Employee}
            };

            var projects = new Project[]
            {
                new Project {Id =1, Name = "Project 1", CreatedOn = DateTime.Now, CreatedBy =1 },
                new Project {Id =2, Name = "Project 2", CreatedOn = DateTime.Now, CreatedBy =1 },
                new Project {Id =3, Name = "Project 3", CreatedOn = DateTime.Now, CreatedBy =2 },
                new Project {Id =4, Name = "Project 4", CreatedOn = DateTime.Now, CreatedBy =2 },
            };

            builder.Entity<Project>().HasData(projects);
            builder.Entity<User>().HasData(users);


            builder.Entity<ProjectMember>()
                .HasKey(pm => new { pm.MemberId, pm.ProjectId });
            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(pm => pm.MemberId);
            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(pm => pm.ProjectId);

            builder.Entity<CardAssign>()
               .HasKey(ca => new { ca.AssignTo, ca.CardId });
            builder.Entity<CardAssign>()
                .HasOne(ca => ca.Card)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(ca => ca.CardId);
            builder.Entity<CardAssign>()
                .HasOne(ca => ca.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(ca => ca.AssignTo);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardAssign> CardAssigns { get; set; }
        public DbSet<CardTag> CardTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }

    }
}
