using Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Data.Entities
{
    public class Product : IBaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("category")]
        public Guid CategoryId { get; set; }

    }
}
