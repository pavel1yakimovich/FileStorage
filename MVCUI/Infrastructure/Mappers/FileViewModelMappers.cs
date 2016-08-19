using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MVCUI.ViewModels.File;

namespace MVCUI.Infrastructure.Mappers
{
    public static class FileViewModelMappers
    {
        public static BllFile ToBllFile(this FileViewModel fileViewModel)
        {
            return new BllFile()
            {
                Id = fileViewModel.Id,
                IsPublic = fileViewModel.IsPublic,
                Name = fileViewModel.Name,
                Content = fileViewModel.Content,
                Description = fileViewModel.Description,
                Date = fileViewModel.Date,
                UserId = fileViewModel.User.Id,
                User = fileViewModel.User.ToBllUser()
            };
        }

        public static FileViewModel ToMvcFile(this BllFile bllFile)
        {
            return new FileViewModel()
            {
                Id = bllFile.Id,
                IsPublic = bllFile.IsPublic,
                Name = bllFile.Name,
                Content = bllFile.Content,
                Description = bllFile.Description,
                Date = bllFile.Date,
                User = bllFile.User.ToMvcUser()
            };
        }
    }
}