using Shared.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Data.Entities
{
    public class Order : IBaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Column("order_id")]
        public Guid ProductId { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}
