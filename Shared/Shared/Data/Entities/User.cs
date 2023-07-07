using Dashboard.Interfaces;
using Dashboard.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class User : IBaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Column("username")]
        public string Username { get; set; }
        [Column("role")]
        public Role Role { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("money")]
        public decimal Money { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
