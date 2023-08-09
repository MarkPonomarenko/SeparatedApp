using AutoMapper;
using Client.Models;
using Shared.Data.Entities;
using Shared.Models.DTO;

namespace Shared.Utils.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping() 
        {
            CreateMap<UserLoginViewModel, UserLoginDTO>();
            CreateMap<UserRegisterViewModel, User>().AfterMap((src, dest) =>
            {
                dest.UserName = src.UserName;
                dest.Email = src.Email;
                dest.Role = Role.Personal;
            });
            CreateMap<User, UserProfileViewModel>();
        }
    }
}
