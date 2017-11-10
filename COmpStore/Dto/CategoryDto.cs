using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class CategoryDto: BaseEntity
    {

        [Required]
        public string CategoryName { get; set; }

        public ICollection<SubCategoryDto> SubCategories { get; set; }
    }
}
