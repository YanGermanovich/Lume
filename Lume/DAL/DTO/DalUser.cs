using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalUser : IEntity
    {
        [CustomAttributeMapper("id_user")]
        public long Id { get; set; }

        [CustomAttributeMapper("users_name")]
        public string UserName { get; set; }

        [CustomAttributeMapper("users_password")]
        public string Password { get; set; }

        [CustomAttributeMapper("users_email")]
        public string Email { get; set; }

        [CustomAttributeMapper("type_user")]
        public string Type { get; set; }

        [CustomAttributeMapper("id_city")]
        public long Id_City { get; set; }

        [CustomAttributeMapper("phone_number")]
        public string PhoneNumber { get; set; }

        [CustomAttributeMapper("id_avatars")]
        public long Id_Avatar { get; set; }

        [CustomAttributeMapper("users_N")]
        public double? N { get; set; }

        [CustomAttributeMapper("users_E")]
        public double? E { get; set; }
    }
}
