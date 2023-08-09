using Shared.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Shared.Interfaces;

namespace Shared.Data.Entities
{
    public class User : IdentityUser
    {
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Column("role")]
        public Role Role { get; set; }
        [Column("money")]
        public decimal Money { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
