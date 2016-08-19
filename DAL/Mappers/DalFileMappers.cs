using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalFileMappers
    {
        public static DalFile ToDalFile(this File ormFile)
        {
            return new DalFile()
            {
                Id = ormFile.Id,
                IsPublic = ormFile.IsPublic,
                Name = ormFile.Name,
                Description = ormFile.Description,
                Content = ormFile.Content,
                Type = ormFile.Type,
                Date = ormFile.Date,
                User = ormFile.User.ToDalUser(),
                UserId = ormFile.User.Id
            };
        }

        public static File ToOrmFile(this DalFile dalFile)
        {
            return new File()
            {
                Id = dalFile.Id,
                IsPublic = dalFile.IsPublic,
                Name = dalFile.Name,
                Description = dalFile.Description,
                Content = dalFile.Content,
                Type = dalFile.Type,
                Date = dalFile.Date,
                User = dalFile.User.ToOrmUser(),
                User_Id = dalFile.UserId
            };
        }
    }
}
