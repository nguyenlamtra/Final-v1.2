using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public Guid PublisherId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Description { get; set; }
        public string MadeIn { get; set; }
    }
}
