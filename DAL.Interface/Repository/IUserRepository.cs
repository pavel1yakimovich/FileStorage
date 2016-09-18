using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        /// <summary>
        /// Method for finding user by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>User</returns>
        DalUser GetByName(string name);
    }
}
