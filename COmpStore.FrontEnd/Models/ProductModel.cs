using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Display (Name = "Product Name")]
        [Required]
        [MaxLength(20)]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Publisher")]
        [Required]
        public int PublisherId { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int SubCategoryId { get; set; }

        [Display(Name = "Description")]
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Display(Name = "MadeIn")]
        [Required]
        [MaxLength(50)]
        public string MadeIn { get; set; }
        public int InStock { get; set; }

        public string Image { get; set; }

        public PublisherModel Publisher { get; set; }
        public SubCategoryModel SubCategory { get; set; }
    }
}
