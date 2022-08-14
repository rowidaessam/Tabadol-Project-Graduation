using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace identityWithChristina.Models
{
    public class OrderDetail
    {
        [Required]
        public int OdrerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter Your Points per unit Please !! ")]
        public int PointsPerUnite { get; set; }

        [Required(ErrorMessage = "Enter Your Discount Please !! ")]
        public double DisCount { get; set; }

        [Required(ErrorMessage = "Enter Your Points Please !! ")]
        public int? NetPoints { get; set; }

        public virtual Order Odrer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
