using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Models
{
    public class PublisherModel
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
