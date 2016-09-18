using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class FileRepository : IRepository<DalFile>
    {
        private readonly DbContext context;

        public FileRepository(DbContext uow)
        {
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

        public void Update(DalFile entity)
        {
            var oldFile = context.Set<File>().Single(u => u.Id == entity.Id);
            oldFile.Date = entity.Date;
            oldFile.Description = entity.Description;
            oldFile.IsPublic = entity.IsPublic;
        }
    }
}
