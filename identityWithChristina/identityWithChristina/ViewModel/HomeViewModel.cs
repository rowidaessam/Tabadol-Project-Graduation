using identityWithChristina.Models;
namespace identityWithChristina.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Product> products { get; set; } = new List<Product>();
        public IEnumerable<GeneralFeedback> generalFeedbacks { get; set; } = new List<GeneralFeedback>();

        public IEnumerable<ApplicationUser> applicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
