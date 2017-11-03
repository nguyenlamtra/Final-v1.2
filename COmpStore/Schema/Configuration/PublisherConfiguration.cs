using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Configuration
{
    public class PublisherConfiguration
    {
        public PublisherConfiguration(EntityTypeBuilder<PublisherEntity> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(s => s.PublisherName).IsRequired().HasMaxLength(50);
        }
    }
}
