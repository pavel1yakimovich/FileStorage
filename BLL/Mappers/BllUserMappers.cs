using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllUserMappers
    {
        public static DalUser ToDalUser(this BllUser userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                About = userEntity.About,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Roles = userEntity.Role.Select(role => role.ToDalRole())
            };
        }

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            return new BllUser()
            {
                Id = dalUser.Id,
                FirstName = dalUser.FirstName,
                LastName = dalUser.LastName,
                About = dalUser.About,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Role = dalUser.Roles.Select(role => role.ToBllRole())
            };
        }
    }
}
