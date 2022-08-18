using Microsoft.AspNetCore.Identity;
using identityWithChristina.Models;
using System.ComponentModel.DataAnnotations;

namespace identityWithChristina.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FeedbackGaveUsers = new HashSet<Feedback>();
            FeedbackTookUsers = new HashSet<Feedback>();
            GeneralFeedbacks = new HashSet<GeneralFeedback>();
            Orders = new HashSet<Order>();
            ProductExchangationUsers = new HashSet<Product>();
            ProductOwnerUsers = new HashSet<Product>();
        }

      //  [Required]
        public int? Ssn { get; set; }

      //  [Required(ErrorMessage = "Enter Your Points Please !! ")]
        public int? Points { get; set; }

       // [Required(ErrorMessage = "Enter The Number Of Exchange Please !! ")]
        public int? NumberOfExchanges { get; set; }

       // [Required(ErrorMessage = "Please Choose User Image")]
        [Display(Name = "Profile Picture")]
        public string? ProfilePictureUrl { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
       // [Required(ErrorMessage = "Max length User Name must be is 50 letter and the min is 3 letter")]
        public string? Lname { get; set; }

       // [Required]
        public string? Gender { get; set; }

      //  [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

       // [Required(ErrorMessage = "Enter Your Country Please !! ")]
        [StringLength(100)]
        public string? Country { get; set; }

       // [Required(ErrorMessage = "Enter Your City Please !! ")]
        [StringLength(100)]
        public string? City { get; set; }

       // [Required(ErrorMessage = "Enter Your Street Please !! ")]
        [StringLength(100)]
        public string? Street { get; set; }



        public virtual ICollection<Feedback> FeedbackGaveUsers { get; set; }
        public virtual ICollection<Feedback> FeedbackTookUsers { get; set; }
        public virtual ICollection<GeneralFeedback> GeneralFeedbacks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> ProductExchangationUsers { get; set; }
        public virtual ICollection<Product> ProductOwnerUsers { get; set; }
    }
}
