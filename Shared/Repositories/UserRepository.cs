using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Data.Entities;
using Shared.Specifications;
using System.Diagnostics;

namespace Shared.Repositories
{
    public class UserRepository : IUserRepositry
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Null context is not valid");
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public Task<User> GetByIdWithIncludeAsync(string id, string[] includeNames)
        {
            if (includeNames == null)
            {
                throw new ArgumentNullException("Include names array is null");
            }
            IQueryable<User> users = _context.Users;
            foreach (var name in includeNames)
            {
                users = users.Include(name);
            }
            return users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<int> GetTotalCountOfUsers()
        {
            return _context.Users.CountAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task<User> GetUserByIdAsync(string id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<User>> GetUserBySpecificationAsync(Specification<User> specification)
        {
            return _context.Users.Where(specification?.ToExpression()).ToListAsync();
        }

        public Task<User> GetUserByUserNameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(x =>x.UserName == userName);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _context.Users.ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
