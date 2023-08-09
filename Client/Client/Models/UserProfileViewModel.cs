using Shared.Data.Entities;
using Shared.Utils;

namespace Client.Models
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; } 
        public string Email { get; set; }
        public Role Role { get; set; }
        public List<Order> Orders { get; set; }
    }
}
