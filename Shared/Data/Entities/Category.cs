using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Interfaces;

namespace Shared.Data.Entities
{
    public class Category : IBaseEntity
    {
        [Key]
        [Column("categ_id")]
        public Guid Id { get; set; }
        [Column("categ_created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Column("categ_money")]
        public decimal SpentMoney { get; set; }
        [Column("categ_name")]
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
