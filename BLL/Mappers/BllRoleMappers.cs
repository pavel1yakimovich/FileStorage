using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllRoleMappers
    {
        public static DalRole ToDalRole(this BllRole roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            };
        }

        public static BllRole ToBllRole(this DalRole bllRole)
        {
            return new BllRole()
            {
                Id = bllRole.Id,
                Name = bllRole.Name
            };
        }
    }
}
