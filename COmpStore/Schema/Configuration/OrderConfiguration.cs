using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Schema.Configuration
{
    public class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<Order> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(s => s.CreateDate).IsRequired();
        }
    }
}
