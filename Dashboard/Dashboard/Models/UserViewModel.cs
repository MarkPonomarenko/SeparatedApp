using Shared.Interfaces;

namespace Dashboard.Models
{
    public class UserViewModel : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public decimal Money { get; set; }
    }
}
