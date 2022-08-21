using identityWithChristina.Models;
namespace identityWithChristina.ViewModel
{
    public class CartViewModel
    {

        public Order Order { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public List<Product> Products { get; set; } = new List<Product>();

    }

}
