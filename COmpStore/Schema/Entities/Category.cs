using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        
        public virtual IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
