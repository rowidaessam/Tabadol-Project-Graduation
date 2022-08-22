using identityWithChristina.Models;
using Microsoft.AspNetCore.Identity;

namespace identityWithChristina.ViewModel
{
    public class AssociationUserProductViewModel
    {


        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Association>? Associations { get; set; }
        public Product Product { get; set; }
        public readonly UserManager<ApplicationUser> userManager;


    }
}
