using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().ToList().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int key)
        {
            var ormUser = context.Set<User>().FirstOrDefault(user => user.Id == key);
            return ormUser.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            var param = Expression.Parameter(typeof(User));
            var body = new Visitor<User>(param).Visit(f.Body);
            Expression<Func<User, bool>> expr = Expression.Lambda<Func<User, bool>>(body, param);
            var user = context.Set<User>().FirstOrDefault(expr);

            return user.ToDalUser();
        }

        public void Create(DalUser dalUser)
        {
            var user = dalUser.ToOrmUser();

            var roles = new List<Role>();

            foreach (var role in user.Roles)
            {
                var ormRole = context.Set<Role>().Find(role.Id);
                roles.Add(ormRole);
            }
            user.Roles = roles;

            context.Set<User>().Add(user);
        }

        public void Delete(DalUser dalUser)
        {
            var user = dalUser.ToOrmUser();

            user = context.Set<User>().Single(u => u.Id == user.Id);
            context.Set<User>().Remove(user);
        }

        public void Update(DalUser entity) // add body of method
        {
            var oldUser = context.Set<User>().Single(u => u.Id == entity.Id);
            oldUser.About = entity.About;
            oldUser.Email = entity.Email;
            oldUser.FirstName = entity.FirstName;
            oldUser.LastName = entity.LastName;
            oldUser.Password = entity.Password;
        }
    }
}