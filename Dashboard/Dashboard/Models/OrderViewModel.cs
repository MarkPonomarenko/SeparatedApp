using Shared.Interfaces;

namespace Dashboard.Models
{
    public class OrderViewModel : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid ProductId { get; set; }
        public bool State { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
    }
}
