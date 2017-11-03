using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Entities
{
    public class Publisher : BaseEntity
    {
        public string PublisherName { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
