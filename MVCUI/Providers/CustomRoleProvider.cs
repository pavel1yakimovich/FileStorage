using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace MVCUI.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string name, string roleName)
        {
            try
            {
                BllUser user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Name == name);

                if (user == null) return false;

                BllRole userRole = RoleService.GetRoleEntity(user.Role.FirstOrDefault().Id); //!!!

                if (userRole != null && userRole.Name == roleName)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public override string[] GetRolesForUser(string name)
        {
            string[] role = new string[] { };
            try
            {
                // Получаем пользователя
                BllUser user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Name == name);
                if (user != null)
                {
                    // получаем роль
                    BllRole userRole = RoleService.GetRoleEntity(user.Role.FirstOrDefault().Id);//!!

                    if (userRole != null)
                    {
                        role = new string[] {userRole.Name};
                    }
                }
            }
            catch
            {
                role = new string[] {};
            }
            return role;
        }

        #region stabs
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
        #endregion
    }
}