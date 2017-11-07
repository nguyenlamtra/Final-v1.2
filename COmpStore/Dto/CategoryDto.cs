using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public IEnumerable<SubCategoryDto> SubCategories { get; set; }
    }
}
