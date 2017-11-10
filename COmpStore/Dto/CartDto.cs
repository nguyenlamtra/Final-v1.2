using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int InStock { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
