using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<ProductEntity> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.ProductName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(p => p.Price).IsRequired();
        }
    }
}
