using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Mappers;
using Lume.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Lume.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IService<BllUser> UserRepository
        {
            get { return (IService<BllUser>)DependencyResolver.Current.GetService(typeof(IService<BllUser>)); }
        }


        public override string ApplicationName { get; set; }


        public override bool IsUserInRole(string email, string roleName)
        {

            UserViewModel user = UserRepository.GetAllEntities().FirstOrDefault(u => u.Email == email).ToUi();

            if (user == null) return false;

            UserRole userRole = user.Role;

            if (userRole.ToString() == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {

            var roles = new string[] { };
            var user = UserRepository.GetFirstByPredicate(u => u.Email==email).ToUi();

            if (user == null) return roles;

            var userRole = user.Role;

            roles = new string[] { userRole.ToString() };

            return roles;

        }



        #region Stabs

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
        #endregion
    }
}