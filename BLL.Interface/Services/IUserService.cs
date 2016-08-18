using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        BllUser GetUserEntity(int id);
        BllUser GetUserEntity(string email);
        IEnumerable<BllUser> GetAllUserEntities();
        void CreateUser(BllUser user);
        void DeleteUser(BllUser user);
        void UpdateUser(BllUser user);
        //etc.
    }
}