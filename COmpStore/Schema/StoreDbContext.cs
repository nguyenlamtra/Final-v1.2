using COmpStore.Schema.Configuration;
using COmpStore.Schema.Entities;
using COmpStore.Schema.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace COmpStore.Schema
{
    public class StoreDbContext : IdentityDbContext<User>
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PublisherConfiguration(modelBuilder.Entity<Publisher>());
            new SubCategoryConfiguration(modelBuilder.Entity<SubCategory>());
            new CategoryConfiguration(modelBuilder.Entity<Category>());
            new ProductConfiguration(modelBuilder.Entity<Product>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
