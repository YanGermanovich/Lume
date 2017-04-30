using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllUser
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("UserName")]
        public string UserName { get; set; }

        [CustomAttributeMapper("Password")]
        public string Password { get; set; }

        [CustomAttributeMapper("Email")]
        public string Email { get; set; }

        [CustomAttributeMapper("Type")]
        public string Type { get; set; }

        [CustomAttributeMapper("Id_City")]
        public long Id_City { get; set; }

        [CustomAttributeMapper("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [CustomAttributeMapper("Id_Avatar")]
        public long Id_Avatar { get; set; }

        [CustomAttributeMapper("N")]
        public double? N { get; set; }

        [CustomAttributeMapper("E")]
        public double? E { get; set; }
    }
}
