using identityWithChristina.Models;

namespace identityWithChristina.ViewModel
{
    public class CategoryProductViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public Product Product { get; set; }
    }
}
