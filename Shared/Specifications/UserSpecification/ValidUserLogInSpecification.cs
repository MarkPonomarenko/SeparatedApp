using Shared.Data.Entities;
using Shared.Models.DTO;
using Shared.Services;
using System.Linq.Expressions;

namespace Shared.Specifications.UserSpecification
{
    public class ValidUserLogInSpecification : Specification<User>
    {
        private readonly UserLoginDTO _userLoginDTO;

        public ValidUserLogInSpecification(UserLoginDTO userLoginDTO)
        {
            _userLoginDTO = userLoginDTO;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            var passwordHash = HashingService.GetPasswordHash(_userLoginDTO.Password);
            return user => user.Email == _userLoginDTO.Email && passwordHash == user.PasswordHash;
        }
    }
}
