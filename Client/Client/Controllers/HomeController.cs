using AutoMapper;
using Shared.Interfaces;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Data.Entities;
using Shared.Models.DTO;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IAccountManager _accountManager;

        private readonly IRepository<Product> _productRepository;

        public HomeController(IMapper mapper, IAccountManager accountManager, IRepository<Product> productRepository)
        {
            _mapper = mapper;
            _accountManager = accountManager;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productsDTO = await _productRepository.GetEntitiesByPage(1, 5).ToListAsync();
            return View(productsDTO);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userViewModel)
        {
            if (ModelState.IsValid && userViewModel != null)
            {
                var userLoginDTO = _mapper.Map<UserLoginDTO>(userViewModel);
                var loginResult = await _accountManager.WebLogIn(userLoginDTO, true);
                if (loginResult.Succeeded)
                {
                    var loggedUser = await _accountManager.UserRepositry.GetUserByEmailAsync(userLoginDTO.Email);
                    if (loggedUser.Role != Shared.Utils.Role.Admin)
                    {
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        //redirect admin to dashboard
                        return Redirect("http://localhost:5081/");
                    }
                }

            }
            return View(userViewModel);
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserRegisterViewModel userRegisterView)
        {
            if (ModelState.IsValid && userRegisterView != null)
            {
                var user = _mapper.Map<User>(userRegisterView);
                var signupResult = await _accountManager.WebRegister(user, userRegisterView.Password);
                if (signupResult.Succeeded)
                {
                    await _accountManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Email", "UserAlreadyExists");
            }
            return View(userRegisterView);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountManager.WebLogOff();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _accountManager.UserRepositry.GetUserByUserNameAsync(User.Identity.Name);
            var userWIncludes = await _accountManager.UserRepositry.GetByIdWithIncludeAsync(user.Id, new string[] { "Orders" });
            var userViewModel = _mapper.Map<UserProfileViewModel>(userWIncludes);
            return View(userViewModel);
        }
    }
}