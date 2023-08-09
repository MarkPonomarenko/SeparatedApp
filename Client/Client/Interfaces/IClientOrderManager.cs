using Shared.Data.Entities;
using Shared.Interfaces;

namespace Client.Interfaces
{
    public interface IClientOrderManager
    {
        IRepository<Order> OrderRepository { get; }
        Task<bool> CreateOrder(Product product);
        Task<bool> Exists(Order order);
    }
}
