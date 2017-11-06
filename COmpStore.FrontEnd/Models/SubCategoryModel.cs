using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
