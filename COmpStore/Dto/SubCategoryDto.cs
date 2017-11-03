using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class SubCategoryDto: BaseEntity
    {
        
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }


    }
}
