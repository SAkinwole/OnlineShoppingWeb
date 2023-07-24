using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingWeb.ViewModel
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "FullName  is Required")]
        public string FullName { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is Required")]
        public string EmailAddress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
