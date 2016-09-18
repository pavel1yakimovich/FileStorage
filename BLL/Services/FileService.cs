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
        private readonly IUserRepository userRepository;

        public FileService(IUnitOfWork uow, IRepository<DalFile> fileRepository, IUserRepository userrRepository)
        {
            this.uow = uow;
            this.fileRepository = fileRepository;
            this.userRepository = userrRepository;
        }

        public BllFile GetFileEntity(int id)
        {
            return fileRepository.GetById(id).ToBllFile();
        }

        public IEnumerable<BllFile> GetAllFileEntities()
        {
            return fileRepository.GetAll().Select(file => file.ToBllFile());
        }

        public IEnumerable<BllFile> GetAllPublicFileEntities()
        {
            var list = fileRepository.GetAll().Where(file => file.IsPublic).Select(file => file.ToBllFile());

            return list;
        }

        public IEnumerable<BllFile> GetAllFileEntitiesOfUser(string user)
        {
            var userService = new UserService(uow, userRepository);
            var owner = userService.GetUserEntity(user);
            try
            {
                var list = fileRepository.GetAll().Where(file => file.UserId == owner.Id)
                    .Select(file => file.ToBllFile()).ToList();

                return list;
            }
            catch (Exception)
            {
                return new List<BllFile>();
            }
        }

        public IEnumerable<BllFile> GetAllPublicFileEntitiesOfUser(string user)
        {
            var userService = new UserService(uow, userRepository);
            var owner = userService.GetUserEntity(user);
            try
            {
                var list = fileRepository.GetAll().Where(file => file.UserId == owner.Id && file.IsPublic).ToList().Select(file => file.ToBllFile());

                return list;
            }
            catch (Exception)
            {
                return new List<BllFile>();
            }
        }

        public void CreateFile(BllFile file)
        {
            fileRepository.Create(file.ToDalFile());
            uow.Commit();
        }

        public void DeleteFile(BllFile file)
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
