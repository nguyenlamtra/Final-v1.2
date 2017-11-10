using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dto
{
    public class ProductDto : BaseEntity
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MadeIn { get; set; }
        [Required]
        public int InStock { get; set; }
        [Required]
        public string Image { get; set; }

        //public PublisherDto Publisher { get; set; }
        //public SubCategoryDto SubCategory { get; set; }
    }
}
