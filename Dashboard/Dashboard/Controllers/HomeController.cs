using Dashboard.Data.Entities;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IRepository<Product> productRepository, IRepository<User> userRepository, IRepository<Order> orderRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders(int page = 1, int pageSize = 5)
        {
            List<OrderViewModel> viewModels = new List<OrderViewModel>();
            List<Order> orders = await _orderRepository.GetAll().ToListAsync();
            foreach (Order order in orders)
            {
                viewModels.Add(new OrderViewModel()
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    UserId = order.UserId,
                    Created = order.Created
                });
            }
            var data = PageData.GetPage(viewModels, await _orderRepository.TotalCountOfEntitiesAsync(), page, pageSize);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderModel createOrderModel)
        {
            if (createOrderModel != null) 
            {
                await _orderRepository.AddAsync(new Order()
                {
                    Id = Guid.NewGuid(),
                    ProductId = createOrderModel.ProductId,
                    UserId = createOrderModel.UserId,
                    Created = DateTime.UtcNow
                }); ;
            }
            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> Products(int page = 1, int pageSize = 5)
        {
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            List<Product> products = await _productRepository.GetAll().ToListAsync();
            foreach (Product product in products)
            {
                viewModels.Add(new ProductViewModel()
                {
                    Id = product.Id,
                    Created = product.Created,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                });
            }
            var data = PageData.GetPage(viewModels, await _productRepository.TotalCountOfEntitiesAsync(), page, pageSize);
            return View(data);
        }

        public async Task<IActionResult> CreateProduct()
        {
            List<CreateProductModel> createModels = new List<CreateProductModel>();
            return View(createModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductModel createModel)
        {
            if (await _productRepository.AddAsync(new Product()
            {
                Created = DateTime.Now,
                Name = createModel.Name,
                Description = createModel.Description,
                Price = createModel.Price,
                CategoryId = createModel.CategoryId
            }))
            {
                await _productRepository.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            Product? product = await _productRepository.GetByIdAsync(id);
            if (product != null) 
            {
                CreateProductModel createModel = new CreateProductModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                };
                return View(createModel);
            }
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExpense(CreateProductModel createModel)
        {
            if (_productRepository.Update(new Product()
            {
                Id = createModel.Id,
                Name = createModel.Name,
                Description = createModel.Description,
                Price = createModel.Price
            }))
            {
                await _productRepository.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");
        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (await _productRepository.DeleteAsync(id)) {
                await _productRepository.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");
        }

        public async Task<IActionResult> Users(int page = 1, int pageSize = 5)
        {
            List<UserViewModel> viewModels = new List<UserViewModel>();
            List<User> users = await _userRepository.GetAll().ToListAsync();
            foreach (User user in users)
            {
                viewModels.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Created = user.Created,
                    Money = user.Money,
                    Role = user.Role.ToString(),
                    Username = user.Username,
                });
            }
            var data = PageData.GetPage(viewModels, await _userRepository.TotalCountOfEntitiesAsync(), page, pageSize);
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}