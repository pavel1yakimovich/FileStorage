using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public DalRole GetById(int key)
        {
            var ormRole = context.Set<Role>().FirstOrDefault(role => role.Id == key);
            return ormRole.ToDalRole();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            var param = Expression.Parameter(typeof(Role));
            var body = new Visitor<Role>(param).Visit(f.Body);
            Expression<Func<Role, bool>> expr = Expression.Lambda<Func<Role, bool>>(body, param);
            var role = context.Set<Role>().FirstOrDefault(expr);

            return role.ToDalRole();
        }
    }
}
