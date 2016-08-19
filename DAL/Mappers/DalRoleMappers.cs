using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalRoleMappers
    {
        public static Role ToOrmRole(this DalRole dalRole)
        {
            return new Role()
            {
                Id = dalRole.Id
            };
        }

        public static DalRole ToDalRole(this Role ormRole)
        {
            return new DalRole()
            {
                Id = ormRole.Id,
                Name = ormRole.Name
            };
        }
    }
}
