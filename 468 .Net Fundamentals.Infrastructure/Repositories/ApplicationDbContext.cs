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
                new User {Id =1, Name = "Tan Trong", Email = "tronglt2001@gmail.com", Role = (int)Role.Employee, ImagePath="https://lh6.googleusercontent.com/X7JYEBXkxFMLWlXgsipqGbOYN6j9Lh_83FdKL-WPAtVKZsNnwrEE-VJVR83IXO73jgq4NrVuwPER2JVgkuyIpFMDMLzN3kbY1uHnD2_5enIx52yB-0IWf_VIfgFcpQBb4Yp3-an0"},
                new User {Id =2, Name = "Hien Nhu", Email = "hiennhu@gmail.com", Role = (int)Role.Employee, ImagePath="https://i.pinimg.com/474x/15/06/df/1506df6aa1b4c6a8162683d7e8114e65.jpg"}
            };

            var projects = new Project[]
            {
                new Project {Id =1, Name = "Project 1", CreatedOn = DateTime.Now, CreatedBy =1 },
                new Project {Id =2, Name = "Project 2", CreatedOn = DateTime.Now, CreatedBy =2 },   
            };

            builder.Entity<Project>().HasData(projects);
            builder.Entity<User>().HasData(users);




            //ProjectMember
            builder.Entity<ProjectMember>().HasKey(pm => new { pm.MemberId, pm.ProjectId });
            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(pm => pm.MemberId);
            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(pm => pm.ProjectId);

            // CardAssign
            builder.Entity<CardAssign>().HasKey(ca => new { ca.CardId, ca.AssignTo});
            builder.Entity<CardAssign>()
                .HasOne(ca => ca.Card)
                .WithMany()
                .HasForeignKey(ca => ca.CardId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CardAssign>()
                .HasOne(ca => ca.User)
                .WithMany()
                .HasForeignKey(ca => ca.AssignTo)
                .OnDelete(DeleteBehavior.NoAction);

            // CardTag
            builder.Entity<CardTag>().HasKey(ct => new { ct.CardId, ct.TagId });
            builder.Entity<CardTag>()
                .HasOne(ct => ct.Card)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(ct => ct.CardId);
            builder.Entity<CardTag>()
                .HasOne(ct => ct.Tag)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(ct => ct.TagId);

        }

        public DbSet<Activity> Activities { get; set; }

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
