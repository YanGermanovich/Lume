using BLL.Entities;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static BllUser ToBll(this UserViewModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return new BllUser()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Type = user.Role.ToString(),
                Id_City = user.Id_City,
                PhoneNumber = user.PhoneNumber,
                Id_Avatar = user.Id_Avatar,
                N = user.N,
                E = user.E
            };
        }

        public static UserViewModel ToUi (this BllUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Role = user.Type == "User" ? UserRole.User : UserRole.Company,
                Id_City = user.Id_City,
                PhoneNumber = user.PhoneNumber,
                Id_Avatar = user.Id_Avatar,
                N = user.N,
                E = user.E
            };
        }

    }
}