using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class UserMapper
    {
        public static BllUser ToBll(this DalUser us)
        {
            if (us == null)
            {
                throw new ArgumentNullException(nameof(us));
            }
            return new BllUser()
            {
                Id = us.Id,
                UserName = us.UserName,
                Password = us.Password,
                Email = us.Email,
                Type = us.Type,
                Id_City = us.Id_City,
                PhoneNumber = us.PhoneNumber,
                Id_Avatar = us.Id_Avatar,
                N = us.N,
                E = us.E
            };
        }

        public static DalUser ToDal(this BllUser us)
        {
            if (us == null)
            {
                throw new ArgumentNullException(nameof(us));
            }
            return new DalUser()
            {
                Id = us.Id,
                UserName = us.UserName,
                Password = us.Password,
                Email = us.Email,
                Type = us.Type,
                Id_City = us.Id_City,
                PhoneNumber = us.PhoneNumber,
                Id_Avatar = us.Id_Avatar,
                N = us.N,
                E = us.E
            };
        }
    }
}
