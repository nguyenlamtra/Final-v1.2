using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class ProductDto : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int PublisherId { get; set; }
        public int SubCategoryId { get; set; }
        public string Description { get; set; }
        public string MadeIn { get; set; }
    }
}
