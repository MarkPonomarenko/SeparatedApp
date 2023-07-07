using Dashboard.Interfaces;

namespace Dashboard.Models
{
    public class ProductViewModel : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
