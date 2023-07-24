using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingWeb.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is Required")]
        public string EmailAddress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
