using Dashboard.Data.Entities;

namespace Dashboard.Models
{
    public class CreateProductModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public List<Category>? AvailableCategories { get; set; }
    }
}
