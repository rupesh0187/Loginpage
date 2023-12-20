using System.ComponentModel.DataAnnotations;

namespace Loginpage.Models
{
    public class LoginVM
    {
        [EmailAddress]
        [Required(ErrorMessage = "please enter Email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; }
    }
}
