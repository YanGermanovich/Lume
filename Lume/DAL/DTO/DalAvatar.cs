using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalAvatar : IEntity
    {
        [CustomAttributeMapper("id_Avatars")]
        public long Id { get; set; }

        [CustomAttributeMapper("avatar_src")]
        public string Src { get; set; }
    }
}
