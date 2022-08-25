using identityWithChristina.Models;

namespace identityWithChristina.ViewModel
{
    public class ProductAssociationViewModel
    {
        public Product product { get; set; }
        public Association association { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
