using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Entities
{
    public class SubCategory : BaseEntity
    {
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
