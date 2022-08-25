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

        [Display(Name = "Association Name")]
        public string Assname { get; set; }

        public string AssDescription { get; set; }

        public string AssAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string AssPhone { get; set; }

        [Display(Name = "Association Image")]
        public string AssLogoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
