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
    public class FileRepository : IRepository<DalFile>
    {
        private ILogger logger;
        private readonly DbContext context;

        public FileRepository(DbContext uow, ILogger log)
        {
            logger = log;
            this.context = uow;
        }

        public void Create(DalFile dalFile)
        {
            var file = dalFile.ToOrmFile();

            var user = context.Set<User>().Find(file.User_Id);
            file.User = user;

            context.Set<File>().Add(file);
        }

        public void Delete(DalFile dalFile)
        {
            var file = dalFile.ToOrmFile();

            file = context.Set<File>().Single(f => f.Id == file.Id);
            context.Set<File>().Remove(file);
        }

        public IEnumerable<DalFile> GetAll()
        {
            return context.Set<File>().ToList().Select(file => file.ToDalFile());
        }

        public DalFile GetById(int key)
        {
            var ormFile = context.Set<File>().FirstOrDefault(file => file.Id == key);
            return ormFile.ToDalFile();
        }

        public DalFile GetByPredicate(Expression<Func<DalFile, bool>> f)
        {
            var param = Expression.Parameter(typeof(File));
            var body = new Visitor<File>(param).Visit(f.Body);
            Expression<Func<File, bool>> expr = Expression.Lambda<Func<File, bool>>(body, param);
            var user = context.Set<File>().FirstOrDefault(expr);

            return user.ToDalFile();
        }

        public void Update(DalFile entity)
        {
            try
            {
                var oldFile = context.Set<File>().Single(u => u.Id == entity.Id);
                oldFile.Date = entity.Date;
                oldFile.Description = entity.Description;
                oldFile.IsPublic = entity.IsPublic;

                logger.Info($"File {oldFile.Name} has been updated.");
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                throw;
            }
        }
    }
}
