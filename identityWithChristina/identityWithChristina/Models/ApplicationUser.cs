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

        public int? Ssn { get; set; }

        public int? Points { get; set; }

        public int? NumberOfExchanges { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePictureUrl { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string? Lname { get; set; }

        public string? Gender { get; set; }

        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

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
