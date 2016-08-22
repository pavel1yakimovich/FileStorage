using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IFileService
    {
        BllFile GetFileEntity(int id);
        IEnumerable<BllFile> GetAllFileEntities();
        IEnumerable<BllFile> GetAllPublicFileEntities();
        IEnumerable<BllFile> GetAllFileEntitiesOfUser(BllUser user);
        IEnumerable<BllFile> GetAllPublicFileEntitiesOfUser(BllUser user);
        void CreateFile(BllFile file);
        void DeleteUser(BllFile file);
        void UpdateFile(BllFile file);
    }
}
