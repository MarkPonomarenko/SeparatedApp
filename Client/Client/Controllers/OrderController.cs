using AutoMapper;
using Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Data.Entities;
using Shared.Interfaces;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;

        public OrderController(IAccountManager accountManager, IRepository<Product> productRepository, IRepository<Order> orderRepository)
        {
            _accountManager = accountManager;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> CreateOrder(Guid id, int quantity)
        {
            var user = await _accountManager.UserRepositry.GetUserByUserNameAsync(User.Identity.Name);
            if (user != null)
            {
                var productToBuy = await _productRepository.GetByIdAsync(id);
                if (productToBuy != null && productToBuy.Quantity > 0)
                {
                    var newOrderGuid = Guid.NewGuid();
                    var newOrder = new Order
                    {
                        ProductId = id,
                        UserId = Guid.Parse(user.Id),
                        Id = newOrderGuid,
                        State = false,
                        Created = DateTime.Now,
                        Quantity = quantity
                    };
                    await _orderRepository.AddAsync(newOrder);
                    await _orderRepository.SaveChangesAsync();
                    user.Orders.Add(await _orderRepository.GetByIdAsync(newOrderGuid));
                    productToBuy.Quantity -= quantity;
                    _orderRepository.Update(newOrder);
                    await _orderRepository.SaveChangesAsync();
                    _accountManager.UserRepositry.Update(user);
                    await _accountManager.UserRepositry.SaveChangesAsync();
                    //notification success
                    return RedirectToAction("Index", "Home");
                }

            }
            //notification failure
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AcceptOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                var orderedProduct = await _productRepository.GetByIdAsync(order.ProductId);
                var user = await _accountManager.UserRepositry.GetUserByUserNameAsync(User.Identity.Name);
                if (user != null && orderedProduct != null)
                {
                    var estimatedPrice = orderedProduct.Price * order.Quantity;
                    if (user.Money >= estimatedPrice)
                    { 
                        user.Money -= orderedProduct.Price * order.Quantity;
                        order.State = true;
                        _accountManager.UserRepositry.Update(user);
                        _orderRepository.Update(order);
                        await _accountManager.UserRepositry.SaveChangesAsync();
                        await _orderRepository.SaveChangesAsync();
                        //notification success
                        return RedirectToAction("Profile", "Home");
                    }
                }
            }
            //notification failure
            return RedirectToAction("Profile", "Home");
        }

        public async Task<IActionResult> DeclineOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                var orderedProduct = await _productRepository.GetByIdAsync(order.ProductId);
                if (orderedProduct != null)
                {
                    orderedProduct.Quantity += order.Quantity;
                    _orderRepository.Update(order);
                    await _orderRepository.SaveChangesAsync();
                }
                await _orderRepository.DeleteAsync(id);
                await _orderRepository.SaveChangesAsync();
                //notification success
                return RedirectToAction("Profile", "Home");
            }
            //notification failure
            return RedirectToAction("Profile", "Home");
        }
    }
}
