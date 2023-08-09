using Client.Interfaces;
using Shared.Data.Entities;
using Shared.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Client.Utils
{
    public class ClientOrderManager : IClientOrderManager
    {
        public IRepository<Order> OrderRepository { get; }

        public Task<bool> CreateOrder([NotNull] Product product)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Exists(Order order)
        {
            return Task.FromResult(true);
        }
    }
}
