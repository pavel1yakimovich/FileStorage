using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MVCUI.ViewModels;
using MVCUI.ViewModels.User;

namespace MVCUI.Infrastructure.Mappers
{
    public static class RoleViewModelMappers
    {
        public static BllRole ToBllRole(this RoleViewModel roleViewModel)
        {
            return new BllRole()
            {
                Id = roleViewModel.Id,
                Name = roleViewModel.Name
            };
        }

        public static RoleViewModel ToMvcRole(this BllRole bllRole)
        {
            return new RoleViewModel()
            {
                Id = bllRole.Id,
                Name = bllRole.Name
            };
        }
    }
}