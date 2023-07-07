namespace Dashboard.Models
{
    public class CreateOrderModel
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
