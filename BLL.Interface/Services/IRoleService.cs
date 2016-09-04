using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Method for getting role by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Role</returns>
        BllRole GetRoleEntity(int id);

        /// <summary>
        /// Method for getting role by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Role</returns>
        BllRole GetRoleEntity(string name);
    }
}
