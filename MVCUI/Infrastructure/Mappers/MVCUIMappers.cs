using BLL.Interface.Entities;
using MVCUI.ViewModels;

namespace MVCUI.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this BllUser userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Role = (ViewModels.Role)userEntity.RoleId
            };
        }

        public static BllUser ToBllUser(this UserViewModel userViewModel)
        {
            return new BllUser()
            {
                Id = userViewModel.Id,
                UserName = userViewModel.UserName,
                RoleId = (int)userViewModel.Role
            };
        }
    }
}