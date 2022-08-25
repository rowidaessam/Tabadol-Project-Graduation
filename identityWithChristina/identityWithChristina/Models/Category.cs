using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identityWithChristina.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [StringLength(40, MinimumLength = 5)]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }

        [StringLength(200, MinimumLength = 20)]
        public string? Description { get; set; }

        [Display(Name = "Category Image")]
        public string? PhotoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
