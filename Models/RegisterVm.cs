using System.ComponentModel.DataAnnotations;

namespace Loginpage.Models
{
    public class RegisterVm
    {
        [EmailAddress]
        [Required(ErrorMessage ="please enter Email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage ="please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Display(Name ="Confirm Passsword")]
        [Required(ErrorMessage ="please enter confirm password")]
        [Compare("Password",ErrorMessage ="Password and confirm password not matched")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
