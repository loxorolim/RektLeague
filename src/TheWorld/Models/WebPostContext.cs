﻿using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace TheWorld.Models
{
    public class WebPostContext : IdentityDbContext<WorldUser>
    {
        public WebPostContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<WebPost> WebPosts { get; set; }  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:WebPostContextConnection"];
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}