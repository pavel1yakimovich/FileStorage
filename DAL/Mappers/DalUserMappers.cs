﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalUserMappers
    {
        public static User ToOrmUser(this DalUser dalUser)
        {
            return new User()
            {
                Id = dalUser.Id,
                Name = dalUser.Name,
                Password = dalUser.Password,
                Roles = dalUser.Roles.Select(role => role.ToOrmRole()).ToList()
            };
        }

        public static DalUser ToDalUser(this User ormUser)
        {
            return new DalUser()
            {
                Id = ormUser.Id,
                Name = ormUser.Name,
                Password = ormUser.Password,
                Roles = ormUser.Roles.ToList().Select(role => role.ToDalRole())
            };
        }
    }
}
