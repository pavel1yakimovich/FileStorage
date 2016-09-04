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
        /// Method for finding role entity by predicate
        /// </summary>
        /// <param name="f">Expression</param>
        /// <returns>Entity</returns>
        DalRole GetByPredicate(Expression<Func<DalRole, bool>> f);
    }
}
