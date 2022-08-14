using identityWithChristina.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identityWithChristina.Models
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }


        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter Your Product Name Please !! ")]
        [StringLength(50)]
        [Display(Name = "Product Name ")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Enter Your Points Please !! ")]
        public int Points { get; set; }

        [Required(ErrorMessage = "Enter Product Description please !! ")]
        [StringLength(200, MinimumLength = 20)]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Please Choose Product Image")]
        [Display(Name = "Product Image")]
        public string PhotoUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string OwnerUserId { get; set; }
        public string ExchangationUserId { get; set; }
        public int? DonationAssId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Association? DonationAss { get; set; }
        public virtual ApplicationUser? ExchangationUser { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
