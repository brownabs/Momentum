using System;
using System.Collections.Generic;
using System.Text;
using Momentum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Momentum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Quote> Quote { get; set; }
        public DbSet<Friendship> Friendship { get; set; }
        public DbSet<ProjectComment> ProjectComment { get; set; }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Abbey",
                LastName = "Brown",
                UserName = "brownabs",
                NormalizedUserName = "ME@ME.COM",
                Email = "me@me.com",
                NormalizedEmail = "me@me.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "67abd6d3-7436-4448-8aec-08d8a5641178",
                Id = "3abf5f13-de66-480c-adad-8d1e1eecf318"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "P8ssword!");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


        }
    }
}
