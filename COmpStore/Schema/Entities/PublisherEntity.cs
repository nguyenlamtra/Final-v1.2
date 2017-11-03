using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Entities
{
    public class PublisherEntity : BaseEntity
    {
        public string PublisherName { get; set; }

        public virtual IEnumerable<ProductEntity> Products { get; set; }
    }
}
