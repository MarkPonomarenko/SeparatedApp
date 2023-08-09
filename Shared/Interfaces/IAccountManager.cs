using Microsoft.AspNetCore.Identity;
using Shared.Data.Entities;
using Shared.Models.DTO;

namespace Shared.Interfaces
{
    public interface IAccountManager
    {
        static User? CurrentUser { get; set; }
        IUserRepositry UserRepositry { get; }
        UserManager<User> UserManager { get; }
        Task<User> GetLoggedInUserAsync(UserLoginDTO userLogin);
        Task<bool> IsExists(UserRegisterDTO userRegisterDTO);
        Task<SignInResult> WebLogIn(UserLoginDTO userLogin, bool remember);
        Task<IdentityResult> WebRegister(User user, string password);
        Task WebLogOff();
        Task SignInAsync(User user, bool isPersistent);
    }
}
