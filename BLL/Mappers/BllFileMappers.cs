using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllFileMappers
    {
        public static DalFile ToDalFile(this BllFile bllFile)
        {
            return new DalFile()
            {
                Id = bllFile.Id,
                IsPublic = bllFile.IsPublic,
                Name = bllFile.Name,
                Content = bllFile.Content,
                Description = bllFile.Description,
                Date = bllFile.Date,
                UserId = bllFile.UserId,
                User = bllFile.User.ToDalUser()
            };
        }

        public static BllFile ToBllFile(this DalFile dalFile)
        {
            return new BllFile()
            {
                Id = dalFile.Id,
                IsPublic = dalFile.IsPublic,
                Name = dalFile.Name,
                Content = dalFile.Content,
                Description = dalFile.Description,
                Date = dalFile.Date,
                UserId = dalFile.UserId,
                User = dalFile.User.ToBllUser()
            };
        }
    }
}
