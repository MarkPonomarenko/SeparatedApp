using Shared.Data.Entities;
using Shared.Specifications;

namespace Shared.Interfaces
{
    public interface IUserRepositry
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetByIdWithIncludeAsync(string id, string[] includeNames);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetUserBySpecificationAsync(Specification<User> specification);
        Task<int> GetTotalCountOfUsers();
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<int> SaveChangesAsync();
    }
}
