using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password confirm field must equal password")]
        public string ConfirmPassword { get; set; }
    }
}
