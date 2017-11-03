using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Configuration
{
    public class SubCategoryConfiguration
    {
        public SubCategoryConfiguration(EntityTypeBuilder<SubCategoryEntity> entityBuilder)
        {
            entityBuilder.HasKey(s => s.Id);
            entityBuilder.Property(s => s.SubCategoryName).IsRequired().HasMaxLength(50);
        }
    }
}
