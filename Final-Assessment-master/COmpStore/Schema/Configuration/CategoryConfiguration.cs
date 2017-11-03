using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Configuration
{
    public class CategoryConfiguration
    {
        public CategoryConfiguration(EntityTypeBuilder<CategoryEntity> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(s => s.CategoryName).IsRequired().HasMaxLength(50);
        }
    }
}
