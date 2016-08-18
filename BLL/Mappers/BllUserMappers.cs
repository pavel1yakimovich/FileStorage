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
            var rolesList = new List<DalRole>();
            rolesList.Add(userEntity.Role.ToDalRole());

            return new DalUser()
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                About = userEntity.About,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Roles = rolesList
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
                Role = dalUser.Roles.FirstOrDefault().ToBllRole()
            };
        }
    }
}
