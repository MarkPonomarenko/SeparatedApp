using Dashboard.Interfaces;

namespace Dashboard.Models
{
    public class OrderViewModel : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
