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
                Type = fileViewModel.Type,
                Description = fileViewModel.Description,
                Date = fileViewModel.Date,
                UserId = fileViewModel.UserId
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
                Type = bllFile.Type,
                Description = bllFile.Description,
                Date = bllFile.Date,
                UserId = bllFile.UserId,
                User = bllFile.User.Name
            };
        }
    }
}