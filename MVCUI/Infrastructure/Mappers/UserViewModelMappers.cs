using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using MVCUI.ViewModels;
using MVCUI.ViewModels.Account;

namespace MVCUI.Infrastructure.Mappers
{
    public static class UserViewModelMappers
    {
        public static UserViewModel ToMvcUser(this BllUser bllUser)
        {
            return new UserViewModel()
            {
                Id = bllUser.Id,
                Name = bllUser.Name,
                Role = bllUser.Role.Select(role => role.ToMvcRole()).FirstOrDefault()
            };
        }

        public static BllUser ToBllUser(this RegisterViewModel registerViewModel)
        {
            return new BllUser()
            {
                Id = registerViewModel.Id,
                Name = registerViewModel.Name,
                Password = registerViewModel.Password,
                Role = new List<BllRole>() { registerViewModel.Role.ToBllRole() }
            };
        }

        public static BllUser ToBllUser(this UserViewModel userViewModel)
        {
            var roles = new List<RoleViewModel> { userViewModel.Role };

            return new BllUser()
            {
                Id = userViewModel.Id,
                Name = userViewModel.Name,
                Role = roles.Select(mvcRole => mvcRole.ToBllRole()).ToList()
            };
        }
    }
}