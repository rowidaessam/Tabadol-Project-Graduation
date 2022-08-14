using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identityWithChristina.Models
{
    public class Association
    {
        public Association()
        {
            Products = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int Assid { get; set; }

        [Required(ErrorMessage = "Max length Association Name must be is 30 letter and the min is 20 letter")]
        [StringLength(30, MinimumLength = 20)]
        [Display(Name = "Association Name")]
        public string Assname { get; set; }

        [StringLength(200, MinimumLength = 20)]
        [Required(ErrorMessage = "Max length Description must be is 200 letter and the min is 20 letter")]
        public string AssDescription { get; set; }

        [Required(ErrorMessage = "Enter the Adress Please !! ")]
        public string AssAddress { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number")]
        public string AssPhone { get; set; }

        [Required(ErrorMessage = "Please Choose Association logo")]
        [Display(Name = "Association Image")]
        public string AssLogoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
