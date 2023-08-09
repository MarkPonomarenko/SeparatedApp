using Shared.Data.Entities;
using Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Specifications.UserSpecification
{
    public class ValidUserRegisterSpecification : Specification<User>
    {
        private readonly UserRegisterDTO _userRegisterDTO;

        public ValidUserRegisterSpecification(UserRegisterDTO userRegisterDTO)
        {
            _userRegisterDTO = userRegisterDTO;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Email == _userRegisterDTO.Email && _userRegisterDTO.Password == _userRegisterDTO.PasswordConfirm;
        }
    }
}
