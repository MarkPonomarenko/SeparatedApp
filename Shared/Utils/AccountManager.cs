using Shared.Interfaces;
using Shared.Repositories;
using Microsoft.AspNetCore.Identity;
using Shared.Data.Entities;
using Shared.Models.DTO;
using Shared.Specifications.UserSpecification;

namespace Shared.Utils
{
    public class AccountManager : IAccountManager
    {

        public IUserRepositry UserRepositry { get; }
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }

        public AccountManager(IUserRepositry repositry)
        {
            UserRepositry = repositry ?? throw new ArgumentNullException("Null repository input");
        }

        public AccountManager(IUserRepositry repositry, UserManager<User> userManager, SignInManager<User> signInManager) : this(repositry)
        {
            UserManager = userManager ?? throw new ArgumentNullException("Null manager input");
            SignInManager = signInManager ?? throw new ArgumentNullException("Null signin manager input");
        }

        public async Task<User> GetLoggedInUserAsync(UserLoginDTO userLogin)
        {
            if (userLogin == null)
            {
                throw new ArgumentNullException("Input user is null");
            }
            var existsLogInUserSpecification = new ValidUserLogInSpecification(userLogin);
            return (await UserRepositry.GetUserBySpecificationAsync(existsLogInUserSpecification)).FirstOrDefault();
        }

        public async Task<bool> IsExists(UserRegisterDTO userRegister)
        {
            var existsUserRegisterSpecification = new ValidUserRegisterSpecification(userRegister);
            var count = (await UserRepositry.GetUserBySpecificationAsync(existsUserRegisterSpecification)).Count();
            return Convert.ToBoolean(count);
        }

        public Task SignInAsync(User user, bool isPersistent)
        {
            return SignInManager.SignInAsync(user, isPersistent);
        }

        public Task<SignInResult> WebLogIn(UserLoginDTO userLogin, bool remember)
        {
            return SignInManager?.PasswordSignInAsync(userLogin?.Email, userLogin.Password, remember, false);
        }

        public Task WebLogOff()
        {
            return SignInManager?.SignOutAsync();
        }

        public async Task<IdentityResult> WebRegister(User user, string password)
        {
            return await UserManager.CreateAsync(user, password);
        }
    }
}
