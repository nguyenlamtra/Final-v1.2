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

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SubCategoryEntity> SubCategories { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PublisherConfiguration(modelBuilder.Entity<PublisherEntity>());
            new SubCategoryConfiguration(modelBuilder.Entity<SubCategoryEntity>());
            new CategoryConfiguration(modelBuilder.Entity<CategoryEntity>());
            new ProductConfiguration(modelBuilder.Entity<ProductEntity>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
