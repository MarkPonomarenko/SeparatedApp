using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class UserLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; } = false;
    }
}
