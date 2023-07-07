using Dashboard.Interfaces;

namespace Dashboard.Models
{
    public class CategoryViewModel : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public decimal TotalMoney { get; set; }
    }
}
