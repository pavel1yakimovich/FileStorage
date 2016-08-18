using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        public BllUser GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public IEnumerable<BllUser> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void CreateUser(BllUser user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(BllUser user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public BllUser GetUserEntity(string email)
        {
            return userRepository.GetByPredicate(user => user.Email == email).ToBllUser();
        }

        public void UpdateUser(BllUser user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }
    }
}
