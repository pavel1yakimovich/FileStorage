using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Method for getting user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        BllUser GetUserEntity(int id);

        /// <summary>
        /// Method for getting user by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>User</returns>
        BllUser GetUserEntity(string name);

        /// <summary>
        /// Method for getting all users
        /// </summary>
        /// <returns>All users</returns>
        IEnumerable<BllUser> GetAllUserEntities();

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user">user</param>
        void CreateUser(BllUser user);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="user">user</param>
        void DeleteUser(BllUser user);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">user</param>
        void UpdateUser(BllUser user);
        //etc.
    }
}