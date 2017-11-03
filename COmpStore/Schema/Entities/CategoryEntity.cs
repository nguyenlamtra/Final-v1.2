using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string CategoryName { get; set; }
        
        public virtual IEnumerable<SubCategoryEntity> SubCategories { get; set; }
    }
}
