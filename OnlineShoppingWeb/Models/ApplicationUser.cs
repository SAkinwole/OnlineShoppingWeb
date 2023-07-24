using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineShoppingWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
