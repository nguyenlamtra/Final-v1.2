using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class CategoryIndexDto
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int SubCategories { get; set; }
    }
}
