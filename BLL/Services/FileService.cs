using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalFile> fileRepository;

        public FileService(IUnitOfWork uow, IRepository<DalFile> repository)
        {
            this.uow = uow;
            this.fileRepository = repository;
        }

        public BllFile GetFileEntity(int id)
        {
            return fileRepository.GetById(id).ToBllFile();
        }

        public IEnumerable<BllFile> GetAllFileEntities()
        {
            return fileRepository.GetAll().Select(file => file.ToBllFile());
        }

        public IEnumerable<BllFile> GetAllPublicFileEntities() // make it through getbypredicate
        {
            var publicFiles = new List<BllFile>();
            var list = fileRepository.GetAll().Where(file => file.IsPublic);

            foreach (var item in list)
            {
                publicFiles.Add(item.ToBllFile());
            }

            return publicFiles;
        }

        public IEnumerable<BllFile> GetAllFileEntitiesOfUser(BllUser user)
        {
            var list = new List<BllFile>();
            try
            {
                while (true)
                {
                    list.Add(fileRepository.GetByPredicate(file => file.UserId == user.Id).ToBllFile());
                }
            }
            catch (Exception)
            {
                return list;
            }
        }

        public void CreateFile(BllFile file)
        {
            fileRepository.Create(file.ToDalFile());
            uow.Commit();
        }

        public void DeleteUser(BllFile file)
        {
            fileRepository.Delete(file.ToDalFile());
            uow.Commit();
        }

        public void UpdateFile(BllFile file)
        {
            fileRepository.Update(file.ToDalFile());
            uow.Commit();
        }
    }
}
