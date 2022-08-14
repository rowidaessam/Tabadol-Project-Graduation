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
        [Required(ErrorMessage = "Max length Category Name must be is 40 letter and the min is 5 letter")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [StringLength(200, MinimumLength = 20)]
        [Required(ErrorMessage = "Max length Description must be is 200 letter and the min is 20 letter")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Choose Category Image")]
        [Display(Name = "Category Image")]
        public string PhotoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
