using System.ComponentModel.DataAnnotations;

namespace identityWithChristina.ViewModel
{
    public class RoleViewModel
    {

        [Required]
        public string Name { get; set; }
    }
}
