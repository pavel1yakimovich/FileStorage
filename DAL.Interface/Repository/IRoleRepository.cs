using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Method for getting role entity by Id
        /// </summary>
        /// <param name="key">id</param>
        /// <returns>Entity</returns>
        DalRole GetById(int key);

        /// <summary>
        /// Method for getting role entity by Name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Entity</returns>
        DalRole GetByName(string name);
    }
}
