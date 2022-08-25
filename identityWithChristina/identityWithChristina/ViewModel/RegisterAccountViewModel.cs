using System.ComponentModel.DataAnnotations;

namespace identityWithChristina.ViewModel
{
    public class RegisterAccountViewModel
    {

        [Required]

      
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage ="noot matched")]
        public string ConfirmPassword { get; set; }
        //public int numberofexchange { get; set; }

    }
}
