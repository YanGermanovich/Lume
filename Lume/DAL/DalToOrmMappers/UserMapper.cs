using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class UserMapper
    {
        public static user ToOrm(this DalUser us)
        {
            if (us == null)
            {
                throw new ArgumentNullException(nameof(us));
            }
            return new user()
            {
                id_user = us.Id,
                users_name = us.UserName,
                users_password = us.Password,
                users_email = us.Email,
                type_user = us.Type,
                id_city = us.Id_City,
                phone_number = us.PhoneNumber,
                id_avatars = us.Id_Avatar,
                users_N = us.N,
                users_E = us.E
            };
        }

        public static DalUser ToDal(this user us)
        {
            if (us == null)
            {
                throw new ArgumentNullException(nameof(us));
            }
            return new DalUser()
            {
                Id = us.id_user,
                UserName= us.users_name,
                Password = us.users_password,
                Email = us.users_email,
                Type= us.type_user,
                Id_City = us.id_city,
                PhoneNumber = us.phone_number,
                Id_Avatar = us.id_avatars,
                N = us.users_N,
                E= us.users_E
            };
        }
    }
}
