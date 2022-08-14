using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identityWithChristina.Models
{
    public class Feedback
    {
       
        [Required(ErrorMessage = "Enter the Gave User Id Please !! ")]
        [Display(Name = "Gave User Id ")]
        public string GaveUserId { get; set; }

        [Required(ErrorMessage = "Enter the Took User Id Please !! ")]
        [Display(Name = " Took User Id  ")]
        public string TookUserId { get; set; }
        public string Message { get; set; } = null!;

        [Required]
        public int Rate { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Feedback Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(GaveUserId))]
        public virtual ApplicationUser GaveUser { get; set; } = null!;

        [ForeignKey(nameof(TookUserId))]
        public virtual ApplicationUser TookUser { get; set; } = null!;
    }
}
