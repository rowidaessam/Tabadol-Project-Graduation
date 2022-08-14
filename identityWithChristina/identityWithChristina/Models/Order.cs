
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identityWithChristina.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ship Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ShipDate { get; set; }

        [Required(ErrorMessage = "Enter the Adress Please !! ")]
        public string ShipAddress { get; set; }

        [Required(ErrorMessage = "Enter the Total Points Please !! ")]
        public int TotalPoints { get; set; }

        [Required(ErrorMessage = "Enter the number of products Please !! ")]
        public int NumberOfProducts { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
