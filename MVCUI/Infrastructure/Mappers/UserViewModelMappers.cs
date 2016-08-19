using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using MVCUI.ViewModels;
using MVCUI.ViewModels.User;

namespace MVCUI.Infrastructure.Mappers
{
    public static class UserViewModelMappers
    {
        public static UserViewModel ToMvcUser(this BllUser bllUser)
        {
            return new UserViewModel()
            {
                Id = bllUser.Id,
                FirstName = bllUser.FirstName,
                LastName = bllUser.LastName,
                Email = bllUser.Email,
                About = bllUser.About,
                Role = bllUser.Role.Select(role => role.ToMvcRole()).FirstOrDefault()
            };
        }

        public static BllUser ToBllUser(this RegisterViewModel registerViewModel)
        {
            return new BllUser()
            {
                Id = registerViewModel.Id,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                About = registerViewModel.About,
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
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                About = userViewModel.About,
                Role = roles.Select(mvcRole => mvcRole.ToBllRole()).ToList()
            };
        }
    }
}